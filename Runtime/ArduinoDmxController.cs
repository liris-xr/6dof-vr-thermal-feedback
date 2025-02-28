# if !UNITY_ANDROID
using System.Collections;
using UnityEngine;
using System;
using System.IO.Ports;
using Debug = UnityEngine.Debug;

//Script that creates the communication between Unity and Arduino
public class ArduinoDmxController : MonoBehaviour
{
    private SerialPort _dmxSerial;
    [SerializeField] private int baudRate = 115200;
    private string _dmxPort = "";
    
    public event Action OnDmxOpen;
    
    private bool _foundDmx;

    public void Start()
    {
        StartCoroutine(OpenPort("DMX"));
    }


    private IEnumerator OpenPort(string handshake)
    {
        yield return FindPort(handshake);
        
        var dmxSerial = new SerialPort(_dmxPort, baudRate)
        {
            Parity = Parity.None,
            StopBits = StopBits.One,
            DataBits = 8,
            DtrEnable = true,
        };
        
        try
        {
            dmxSerial.Open();
        }
        catch (Exception)
        {
            Debug.LogError("DMX is not detected : check serial ports");
        }
        
        yield return new WaitForSeconds(2);

        if (dmxSerial.IsOpen)
        {
            _dmxSerial = dmxSerial;
            OnDmxOpen?.Invoke();
        }
        
        yield return null;
    }

    private IEnumerator FindPort(string handShake)
    {
        while (_foundDmx == false)
        {
            string[] portList = SerialPort.GetPortNames();
            foreach (string port in portList)
            {
                if (port != "COM1")
                {
                    try
                    {
                        var currentPort = new SerialPort(port, baudRate)
                        {
                            Parity = Parity.None,
                            StopBits = StopBits.One,
                            DataBits = 8,
                            DtrEnable = true,
                        };
                        
                        if (currentPort.IsOpen) continue;
                        
                        currentPort.Open();
                        currentPort.WriteLine(handShake);
                        string received = currentPort.ReadLine();
                        currentPort.Close();
                        
                        Debug.Log(received);
                        Debug.Log(handShake);
                        
                        if (!received.Contains(handShake)) continue;
                        
                        Debug.Log("DMX found on " + port);
                        _dmxPort = port;
                        _foundDmx = true;
                    }
                    catch (Exception)
                    {
                    }
                }
            }

            yield return null;
        }

        yield return null;
    }

    public void SendData(string data = "0", string address = "0")
    {
        if (_dmxSerial != null)
        {
            try
            {
                _dmxSerial.WriteLine(address + ';' + data);
                Debug.Log("Data Sent : " + address + ';' + data);
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
        }
    }
    
    private void OnApplicationQuit()
    {
        SendData();
    }
}
#endif