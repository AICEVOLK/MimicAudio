using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderManager : MonoBehaviour
{
    public Slider slider; // スライダー
    public GameObject List;

    public Controller script;
    public AudioSource bgm;

    public float musicTime; // 現在の曲の時間
    public float max; // 再生している曲が終わる時間

    public bool hold; // 曲を自由なタイミングで流せるようにしたいねぇ

    public GameObject timer; // 時間表示オブジェクトを取得
    public Text timerText; // 時間テキストを取得
    public int minutes = 0; // 時間の分の表記
    public float seconds = 0; // 時間の秒の表記
    public float oldSeconds = 0; // 前のUpdate時の秒数
    public float totalSeconds = 0; // 秒数の合計

    public GameObject record; // レコードのオブジェクトを取得

    // Start is called before the first frame update
    void Start() {
        /* 
            この先取得処理
         */
        slider = this.gameObject.GetComponent<Slider>();
        List = GameObject.Find("/Canvas/List");
        script = List.GetComponent<Controller>();
        bgm = List.GetComponent<AudioSource>();

        timer = GameObject.Find("/Canvas/AudioObject/MusicTime");
        timerText = timer.GetComponent<Text>();
        minutes = 0;
        seconds = 0f;
        oldSeconds = 0f;

        record = GameObject.Find("/Canvas/Record");
    }

    // Update is called once per frame
    void Update() {
        if (hold == true) { // ハンドルの現在地を取得してスライダーの値に入れ込みたいね　12/19追記：できたわ
            if (slider.value < max - 0.5f) {
                bgm.Pause();
                musicTime = slider.value;
                // Debug.Log(bgm.time);
            } else {
                slider.value = max - 0.5f;
            }
            
        } else {
            musicTime = bgm.time; // 現在の再生時間
            slider.value = musicTime; // 現在の再生時間をバーに反映

            max = bgm.clip.length; // 曲の長さ
            slider.maxValue = max; // 曲の長さをバーに反映
        }

        if (script.pause == true || script.input == true || hold == true) {
            record.transform.Rotate(new Vector3(0, 0,Time.deltaTime * 0));
        } else {
            record.transform.Rotate(new Vector3(0, 0, Time.deltaTime * -100));
        }

        TimerSetting(); // タイマーを動かす関数

    }

    public void TimerSetting() { // タイマーを動かす関数
        totalSeconds = (int)musicTime;
        seconds = totalSeconds % 60;
        timerText.text = minutes.ToString("0") + ":" + seconds.ToString("00");

        if (totalSeconds < 60) { // 1分過ぎると1:00表記になる
            minutes = 0;
        } else if (totalSeconds >= 60) {
            minutes = 1;
        }
    }

    public void Dawn() { // ボタンを離した時にhold = falseになるようにしたいね 12/19追記：できたわ
        hold = true;
        // Debug.Log("ボタンを押した時間は" + musicTime);
        oldSeconds = totalSeconds;
    }

    public void Up() {
        if (script.pause == false) {
            bgm.Play();
        }
        hold = false;
        bgm.time = musicTime;
    }

    public void Drag() {
        if (hold == true) {
            if (oldSeconds != seconds) {
                record.transform.Rotate(new Vector3(0, 0, (oldSeconds - seconds) * 10));
                oldSeconds = totalSeconds;
            }
        }
    }

}
