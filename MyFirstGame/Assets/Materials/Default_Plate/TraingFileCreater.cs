using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class TraingFileCreater : MonoBehaviour
{
    GameObject N1;
    GameObject N2;
    GameObject N3;
    GameObject N4;
    GameObject N5;
    GameObject N6;
    GameObject N7;

    GameObject K1;
    
    public GameObject Capturecamera;
    public int EachImageCount;

    string [] num = {"0","1","2","3","4","5","6","7","8","9"};
    string [] kor = {"가","나","다","라","마","거","너","더","러","머","버","서","어","저","고","노","도","로","모","보","소","오","조","구","누","두","루","무","부","수","우","주","하","허","호","배","아","바","사","자"};
    // string [] kor2eng = {"ga","na","da","ra","ma","geo","neo","deo","reo","meo","beo","seo","eo","jeo","go","no","do","ro","mo","bo","so","o","jo","gu","nu","du","ru","mu","bu","su","u","ju","ha","heo","ho","bae","a","ba","sa","ja"}    

    // 이미지 get Count
    private Dictionary<string,int> cnt;
    
    float timer;
    double waitingTime;

    // Start is called before the first frame update
    void Start()
    {
        N1 = GameObject.Find("N1");
        N2 = GameObject.Find("N2");
        N3 = GameObject.Find("N3");
        N4 = GameObject.Find("N4");
        N5 = GameObject.Find("N5");
        N6 = GameObject.Find("N6");
        N7 = GameObject.Find("N7");
        K1 = GameObject.Find("K1");

        timer = 0.0f;
        waitingTime = 0.2;
        
        DicInit();
    }    

    // Update is called once per frame
    void Update()
    {
        // RandomPlate();            
        timer += Time.deltaTime;

        if(timer > waitingTime){
            RandomPlate();            
            timer = 0;
        }

        string result = "";
        int check = 0;

        foreach (KeyValuePair<string, int> each in cnt){
            if(each.Value == 0){
                result += each.Key+", ";
                Debug.Log(result);
            }

            if(each.Value >= EachImageCount){
                check += 1;        
            }
        }

        if(check == (num.Length+kor.Length)){
            result = "종료 해라이 다 모았다!!!!";
            Debug.Log(result);        
        }
    }

    /** 카운팅 할 dic 초기화 함수**/
    void DicInit(){
        cnt = new Dictionary<string, int>();

        for(int i = 0 ; i < num.Length; i++){
            cnt.Add(num[i],0);
        }
        for(int i = 0; i< kor.Length; i++){
            cnt.Add(kor[i],0);
        }
    }
    int getRandomNum(){
        return Random.Range(0,num.Length);
    }

    int getRandomKor(){
        return Random.Range(0,kor.Length);
    }

    void RandomPlate(){
        int n1 = getRandomNum();
        int n2 = getRandomNum();
        int n3 = getRandomNum();
        int n4 = getRandomNum();
        int n5 = getRandomNum();
        int n6 = getRandomNum();
        int n7 = getRandomNum();
        int k1 = getRandomKor();

        N1.GetComponent<MeshRenderer>().material = Resources.Load(num[n1], typeof(Material)) as Material;
        N2.GetComponent<MeshRenderer>().material = Resources.Load(num[n2], typeof(Material)) as Material;
        N3.GetComponent<MeshRenderer>().material = Resources.Load(num[n3], typeof(Material)) as Material;
        N4.GetComponent<MeshRenderer>().material = Resources.Load(num[n4], typeof(Material)) as Material;
        N5.GetComponent<MeshRenderer>().material = Resources.Load(num[n5], typeof(Material)) as Material;
        N6.GetComponent<MeshRenderer>().material = Resources.Load(num[n6], typeof(Material)) as Material;
        N7.GetComponent<MeshRenderer>().material = Resources.Load(num[n7], typeof(Material)) as Material;

        K1.GetComponent<MeshRenderer>().material = Resources.Load(kor[k1], typeof(Material)) as Material;

        /** 캡처 **/
        Capturecamera.GetComponent<ScreenShot>().ClickScreenShot(n1,n2,n3,n4,n5,n6,n7,k1);
        /** 마커 파일 생성 **/
        WriteMark(n1,n2,n3,n4,n5,n6,n7,k1);
        
        /** 카운팅 **/
        cnt[num[n1]] = cnt[num[n1]] + 1;        
        cnt[num[n2]] = cnt[num[n2]] + 1;        
        cnt[num[n3]] = cnt[num[n3]] + 1;        
        cnt[num[n4]] = cnt[num[n4]] + 1;        
        cnt[num[n5]] = cnt[num[n5]] + 1;        
        cnt[num[n6]] = cnt[num[n6]] + 1;        
        cnt[num[n7]] = cnt[num[n7]] + 1;        
        cnt[kor[k1]] = cnt[kor[k1]] + 1;        
    }

    void WriteMark(int n1, int n2,int n3,int n4,int n5,int n6,int n7,int k1){
        int ck1 = k1 + 10;
        // string markPath = Application.dataPath+"/ScreenShot/" + "default_plate_"+n1+n2+n3+ck1+n4+n5+n6+n7 + ".txt";
        string markPath = "C:\\Users\\winitech\\Desktop\\TF\\RCNN\\Unity_Captured_txt\\"+"default_plate_"+ck1+n1+n2+n3+n4+n5+n6+n7 + ".txt";

        /** txt 파일 없으면 파일 생성**/
        if(false == File.Exists(markPath)){
            var file = File.CreateText(markPath);
            file.Close();
        }

        string val = n1+" 0.129688 0.534028 0.101562 0.306944\n"+
                    n2+" 0.231641 0.534722 0.100781 0.308333\n"+
                    n3+" 0.332031 0.533333 0.101562 0.305556\n"+
                    ck1+" 0.452344 0.531250 0.139062 0.309722\n"+
                    n4+" 0.571875 0.533333 0.103125 0.305556\n"+
                    n5+" 0.674609 0.532639 0.102344 0.306944\n"+
                    n6+" 0.776563 0.532639 0.101562 0.309722\n"+
                    n7+" 0.877734 0.533333 0.100781 0.308333\n";

        /** mark 파일 작성 **/
        StreamWriter sw = new StreamWriter(markPath);
        sw.Write(val);
        sw.Flush();
        sw.Close();
    }
}
