using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
# if PLATFORM_ANDROID
using UnityEngine.Android;
# endif

public class packet_test : MonoBehaviour
{

    public Text GPS;

    public static double first_Lat; //최초 위도
    public static double first_Long; //최초 경도
    public static double current_Lat; //현재 위도
    public static double current_Long; //현재 경도

    private static WaitForSeconds second;

    private static bool gpsStarted = false;

    private static LocationInfo location;

    private void Awake()
    {
        second = new WaitForSeconds(1.0f);

#if PLATFORM_ANDROID

        if (!Permission.HasUserAuthorizedPermission(Permission.CoarseLocation) ||
            !Permission.HasUserAuthorizedPermission(Permission.Microphone) ||
            !Permission.HasUserAuthorizedPermission(Permission.FineLocation) ||
            !Permission.HasUserAuthorizedPermission(Permission.Camera) ||
            !Permission.HasUserAuthorizedPermission(Permission.ExternalStorageWrite) ||
            !Permission.HasUserAuthorizedPermission(Permission.ExternalStorageRead)
            )
        {

            string[] permissions = {
            Permission.CoarseLocation ,
            Permission.Microphone ,
            Permission.FineLocation,
            Permission.Camera,
            Permission.ExternalStorageWrite,
            Permission.ExternalStorageRead
            };
            //Permission.RequestUserPermissions(permissions);
            Permission.RequestUserPermission(Permission.CoarseLocation);
            Permission.RequestUserPermission(Permission.Microphone);
            Permission.RequestUserPermission(Permission.FineLocation);
            Permission.RequestUserPermission(Permission.Camera);
            Permission.RequestUserPermission(Permission.ExternalStorageWrite);
            Permission.RequestUserPermission(Permission.ExternalStorageRead);

        }
#endif
    }

    IEnumerator Start()
    {
        // 유저가 GPS 사용중인지 최초 체크
        if (!Input.location.isEnabledByUser)
        {
            Debug.Log("GPS is not enabled");
            GPS.text += "\nGPS is not enabled";
            yield break;
        }

        GPS.text += "\nGPS Enable OK";

        //GPS 서비스 시작
        Input.location.Start();
        Debug.Log("Awaiting initialization");

        //활성화될 때 까지 대기
        int maxWait = 20;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return second;
            maxWait -= 1;
        }

        //20초 지날경우 활성화 중단
        if (maxWait < 1)
        {
            Debug.Log("Timed out");
            yield break;
        }

        //연결 실패
        if (Input.location.status == LocationServiceStatus.Failed)
        {
            Debug.Log("Unable to determine device location");
            yield break;

        }
        else
        {
            //접근 허가됨, 최초 위치 정보 받아오기
            location = Input.location.lastData;
            first_Lat = location.latitude * 1.0d;
            first_Long = location.longitude * 1.0d;
            gpsStarted = true;

            //현재 위치 갱신
            while (gpsStarted)
            {
                location = Input.location.lastData;
                current_Lat = location.latitude * 1.0d;
                current_Long = location.longitude * 1.0d;
                yield return second;
            }
        }
    }

    //위치 서비스 종료
    public static void StopGPS()
    {
        if (Input.location.isEnabledByUser)
        {
            gpsStarted = false;
            Input.location.Stop();
        }
    }

    int count;

    public void OnMyPlayerPacket()
    {


        //Debug.Log("OnMyPlayerPacket + (NetID)" + GameObjectManager.instance.my_player.netId);
        //GameObjectManager.instance.my_player.CmdTest();
        count++;
        GPS.text = "";
        GPS.text += "\ncount: " + count;
        GPS.text += "\ns lon: " + 127.09f;//NativeToolkit.GetLongitude().ToString();
        GPS.text += "\ns lat: " + 37.55f;//NativeToolkit.GetLatitude().ToString();
        GPS.text += "\nLongitude: " + current_Long;//NativeToolkit.GetLongitude().ToString();
        GPS.text += "\nLatitude: " + current_Lat;//NativeToolkit.GetLatitude().ToString();

        double distance = Distance(current_Long, current_Lat, 127.09f, 37.55f);
        GPS.text += "\n Dis : " + Mathf.RoundToInt((float)distance) + "km";
       
    }

    public double Distance(double lon1, double lat1, double lon2, double lat2)
    {
        double theta, dist;
        theta = lon1 - lon2;

        dist = Mathf.Sin((float)deg2rad(lat1)) * Mathf.Sin((float)deg2rad(lat2)) +
            Mathf.Cos((float)deg2rad(lat1)) * Mathf.Cos((float)deg2rad(lat2)) * Mathf.Cos((float)deg2rad(theta));
        dist = Mathf.Acos((float)dist);
        dist = rad2deg(dist);
        dist = dist * 60 * 1.1515f;
        dist = dist * 1.609344f;

        return dist;
    }

    private double deg2rad(double deg)
    {
        return (double)(deg * Mathf.PI / (double)180d);
    }

    private double rad2deg(double rad)
    {
        return (double)(rad * (double)180d / Mathf.PI);
    }
}
