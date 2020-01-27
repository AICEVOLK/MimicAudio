using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Controller : MonoBehaviour
{
    public int num; // 最も重要な数値

    public Information information;

    public GameObject List;
    public GameObject[] Characters;

    /* 
        ここから先BGMについて
     */
    public AudioSource bgm;
    public AudioClip clip;

    /* 
        ここからレコードについて
     */
    public GameObject Record;
    // public Animator r_anim;

    /* 
        ここから先ジャケットについて
     */
    public GameObject JacketObj;
    public Sprite jacket;
    // public float speed; // ディスクのスピード

    /* 
        ここから先キャラクターについて
     */
    public GameObject CharacterImage;
    public GameObject CharacterMotion;

    /* 
        ここから先スライダーについて
     */
    public GameObject Slider;
    public SliderManager slider;

    /* 
        ここから先テキストについて
     */
    public GameObject CharacterName;
    public string c_name;

    public GameObject j_nameObj;
    public string j_name;

    public GameObject MusicTitle;
    public string m_title;

    public GameObject j_titleObj;
    public string j_title;

    /* 
        ここから先カーテンについて
     */
    public GameObject curtain;
    public Animator c_anim;

    public bool input; // 入力されたかどうかを判断するためのbool
    public bool loop; // ループするかどうか
    public bool pause; // 一時停止するかどうか

    public GameObject LoopObj; // ループマーク
    public GameObject PauseObj; // 一時停止マーク
    public SEclip SE; // 一時停止時の効果音

    /* 
        ここから先隠しギミック
     */
    // キャラクターの色変更
    public bool color2; // 2pカラーに変更

    // キャラクターがダメージを受ける
    public bool damage; // タッチしたかどうかを判断するためのbool
    public int touchCounter; // タッチするとカウントする
    public float time;

    // Start is called before the first frame update
    void Start() {
        /* 
            Charactersに子オブジェクトを配列するためのスクリプト
         */
        List = GameObject.Find ("/Canvas/List");
        Characters = new GameObject[List.transform.childCount];
        for (int i = 0; i < List.transform.childCount; i++) {
            Characters[i] = List.transform.GetChild(i).gameObject;
        }

        num = 0; // 始めは0と決まっている
        
        /* 
            この先取得する動作
         */
        information = Characters[num].GetComponent<Information>(); // スクリプトを取得
        bgm = this.GetComponent<AudioSource>(); // オーディオを取得
        CharacterImage = GameObject.Find("/Canvas/CharacterImage"); // キャラクターの立ち絵を取得
        CharacterMotion = GameObject.Find("/Canvas/CharacterMotion"); // キャラクターモーションのゲームオブジェクトを取得
        Slider = GameObject.Find("/Canvas/AudioObject/Slider"); // スライダーを取得
        slider = Slider.GetComponent<SliderManager>(); // スライダーのスクリプトを取得
        CharacterName = GameObject.Find("/Canvas/CharacterName"); // キャラクター名を取得
        j_nameObj = GameObject.Find("/Canvas/CharacterName/j_name"); // キャラクター名日本語表記
        MusicTitle = GameObject.Find("/Canvas/MusicTitle"); // 曲名を取得
        j_titleObj = GameObject.Find("/Canvas/MusicTitle/j_title"); // 曲名日本語表記
        Record = GameObject.Find("/Canvas/Record"); // レコードのオブジェクトを取得
        JacketObj = GameObject.Find("/Canvas/Record/Jacket"); // ジャケットのオブジェクトを取得
        curtain = GameObject.Find("/Canvas/Curtain"); // カーテンを取得
        c_anim = curtain.GetComponent<Animator>(); // カーテンのアニメーションを取得

        LoopObj = GameObject.Find("/Canvas/AudioObject/Mark/loop"); // ループマークのオブジェクトを取得する
        LoopObj.GetComponent<Image>().color = new Color32(0, 0, 0, 255); // ループマークを黒にする
        
        PauseObj = GameObject.Find("/Canvas/AudioObject/Mark/pause"); // ポーズマークのオブジェクトを取得する
        PauseObj.GetComponent<Image>().color = new Color32(0, 0, 0, 255); // ポーズマークを黒にする
        SE = PauseObj.GetComponent<SEclip>(); // ポーズマークのスクリプトを取得

        /* 
            この先BGM再生
         */
        clip = information.clip;
        bgm.clip = clip;
        bgm.Play();
        /* 
            この先キャラクター立ち絵表示
         */
        CharacterImage.GetComponent<Image>().sprite = Characters[num].GetComponent<Image>().sprite;

        /* 
            この先キャラクター名表示
         */
        c_name = Characters[num].transform.name;
        CharacterName.GetComponent<Text>().text = c_name;

        j_name = Characters[num].GetComponent<Information>().jpn_name;
        j_nameObj.GetComponent<Text>().text = j_name;

        /* 
            この先曲名表示
         */
        m_title = Characters[num].GetComponent<Information>().title;
        MusicTitle.GetComponent<Text>().text = m_title;

        j_title = Characters[num].GetComponent<Information>().jpn_title;
        j_titleObj.GetComponent<Text>().text = j_title;

        /* 
            この先ジャケット表示
         */
        jacket = Characters[num].GetComponent<Information>().jacket;
        JacketObj.GetComponent<Image>().sprite = jacket;
        // speed = bgm.time; // ジャケットのまわるスピードを曲に依存させる

        /* 
            この先レコードアニメーションについて
         */
        // r_anim = Record.GetComponent<Animator>();

    }

    public void Back() {
        bgm.Pause();
        num -= 1;
        if (num < 0) {
            num = Characters.Length - 1;
        }

        if (num > Characters.Length - 1) {
            num = 0;
        }
        Setting();
    }

    public void Next() {
        // Debug.Log("Next時の曲の時間は" + slider.musicTime);
        bgm.Pause();
        num += 1;
        if (num < 0) {
            num = Characters.Length - 1;
        }

        if (num > Characters.Length - 1) {
            num = 0;
        }
        // Debug.Log("Next時の番号は" + num);
        Setting();
    }

    public void Loop() {
        bgm.Stop();
        Setting();
    }

    public void Setting() {
        bgm.Pause();

        if (bgm.clip == information.clip) {
            information = Characters[num].GetComponent<Information>();
            clip = information.clip;
            bgm.clip = clip;
        }
        CharacterImage.GetComponent<Image>().sprite = Characters[num].GetComponent<Image>().sprite;

        c_name = Characters[num].transform.name;
        CharacterName.GetComponent<Text>().text = c_name;

        j_name = Characters[num].GetComponent<Information>().jpn_name;
        j_nameObj.GetComponent<Text>().text = j_name;

        m_title = Characters[num].GetComponent<Information>().title;
        MusicTitle.GetComponent<Text>().text = m_title;

        j_title = Characters[num].GetComponent<Information>().jpn_title;
        j_titleObj.GetComponent<Text>().text = j_title;

        jacket = Characters[num].GetComponent<Information>().jacket;
        JacketObj.GetComponent<Image>().sprite = jacket;

        Invoke("LateSetting", 0.5f);
    
    }

    public void LateSetting() {
        c_anim.SetBool("action", false); // カーテンを開ける
        CharacterMotion.GetComponent<Image>().sprite = information.images[0]; // キャラクター画像を取得
        // r_anim.SetFloat("MovingSpeed", 1.0f); // レコードを回す
        bgm.Play(); // 曲を流す

        /* 
        　これより下ありとあらゆる値をリセットする
         */
        input = false;
        pause = false;
        damage = false;
        color2 = false;
        touchCounter = 0;

        bgm.time = 0; // これやっとかないとスライドバー動かした後おかしくなる

        SE.on_SE = false;
        PauseObj.GetComponent<Image>().color = new Color32(0, 0, 0, 255);

        // Debug.Log("Controller内のbgm.timeは" + slider.bgm.time); // ←こいつがスライドバー戻らなくなる犯人だった
    }

    public void MotionSetting() {
        if (damage == true) { // ダメージ判定
            time += Time.deltaTime; // ダメージを受けてから何秒経ったか
            if (color2 == true) {
                CharacterMotion.GetComponent<Image>().sprite = information.damage2;
                if (touchCounter == 5) {
                    CharacterMotion.GetComponent<Image>().sprite = information.lose2;
                }
                Reset();
            } else {
                CharacterMotion.GetComponent<Image>().sprite = information.damage;
                if (touchCounter == 5) {
                    CharacterMotion.GetComponent<Image>().sprite = information.lose;
                }
                Reset();
            }
        } else {
            if (color2 == true) {
                if (slider.musicTime >= slider.max / 6 * 5) {
                    CharacterMotion.GetComponent<Image>().sprite = information.images2[5];
                    // Debug.Log("勝利モーション");
                } else if (slider.musicTime >= slider.max / 6 * 4) {
                    CharacterMotion.GetComponent<Image>().sprite = information.images2[4];
                    // Debug.Log("黄");
                } else if (slider.musicTime >= slider.max / 6 * 3) {
                    CharacterMotion.GetComponent<Image>().sprite = information.images2[3];
                    // Debug.Log("紫");
                } else if (slider.musicTime >= slider.max / 6 * 2) {
                    CharacterMotion.GetComponent<Image>().sprite = information.images2[2];
                    // Debug.Log("緑");
                } else if (slider.musicTime >= slider.max / 6) {
                    CharacterMotion.GetComponent<Image>().sprite = information.images2[1];
                    // Debug.Log("青");
                } else {
                    CharacterMotion.GetComponent<Image>().sprite = information.images2[0];
                    // Debug.Log("赤");
                }
            } else {
                if (slider.musicTime >= slider.max / 6 * 5) {
                    CharacterMotion.GetComponent<Image>().sprite = information.images[5];
                    // Debug.Log("勝利モーション");
                } else if (slider.musicTime >= slider.max / 6 * 4) {
                    CharacterMotion.GetComponent<Image>().sprite = information.images[4];
                    // Debug.Log("黄");
                } else if (slider.musicTime >= slider.max / 6 * 3) {
                    CharacterMotion.GetComponent<Image>().sprite = information.images[3];
                    // Debug.Log("紫");
                } else if (slider.musicTime >= slider.max / 6 * 2) {
                    CharacterMotion.GetComponent<Image>().sprite = information.images[2];
                    // Debug.Log("緑");
                } else if (slider.musicTime >= slider.max / 6) {
                    CharacterMotion.GetComponent<Image>().sprite = information.images[1];
                    // Debug.Log("青");
                } else {
                    CharacterMotion.GetComponent<Image>().sprite = information.images[0];
                    // Debug.Log("赤");
                }
            }
        }

    }

    // Update is called once per frame
    void Update() {
        if (input == false) {
            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) { // 前の曲に戻る
                input = true;
                // r_anim.SetFloat("MovingSpeed", 0.0f);
                bgm.Pause();
                c_anim.SetBool("action", true);

                Invoke("Back", 0.5f);
            }
        
            if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) { // 次の曲へ進む
                input = true;
                // r_anim.SetFloat("MovingSpeed", 0.0f);
                bgm.Pause();
                c_anim.SetBool("action", true);

                Invoke("Next", 0.5f);
            }

            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) { // ループ判定
                if (loop == false) {
                    loop = true;
                } else {
                    loop = false;
                }
            } // 後で表示する

            if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) { // ポーズ判定
                if (pause == false) {
                    pause = true;
                    // r_anim.SetFloat("MovingSpeed", 0.0f); // 一時停止
                    bgm.Pause();
                } else {
                    pause = false;
                    // r_anim.SetFloat("MovingSpeed", 1.0f); // 再開
                    bgm.Play();
                }
            } // 後で表示する

            if (slider.musicTime >= slider.max - 0.2f) { // 今の曲が終わったら次の曲を流す
                input = true;
                // r_anim.SetFloat("MovingSpeed", 0.0f);
                bgm.Pause();
                c_anim.SetBool("action", true);

                if (loop == true) {
                    Invoke("Loop", 0.5f);
                } else {
                    Invoke("Next", 0.5f);
                }
                
            }
        }

        /* if (slider.hold == true) {
            // r_anim.enabled = false;
        } else {
            // r_anim.enabled = true;
        } */
        MotionSetting();
    }

    public void BackButton() {
        input = true;
        bgm.Pause();
        c_anim.SetBool("action", true);

        Invoke("Back", 0.5f);
    }

    public void NextButton() {
        input = true;
        bgm.Pause();
        c_anim.SetBool("action", true);

        Invoke("Next", 0.5f);
    }

    public void LoopButton() { // 色変えたい
        if (loop == false) {
            loop = true;
            LoopObj.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        } else {
            loop = false;
            LoopObj.GetComponent<Image>().color = new Color32(0, 0, 0, 255);
        }
    }

    public void PauseButton() {
        if (pause == false) {
            pause = true;
            // r_anim.SetFloat("MovingSpeed", 0.0f); // 一時停止
            PauseObj.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            bgm.Pause();
        } else {
            pause = false;
            // r_anim.SetFloat("MovingSpeed", 1.0f); // 再開
            PauseObj.GetComponent<Image>().color = new Color32(0, 0, 0, 255);
            bgm.Play();
        }
    }

    public void ColorChangeButton() {
        if (color2 == false) {
            color2 = true;
            CharacterImage.GetComponent<Image>().sprite = information.image2;
        } else {
            color2 = false;
            CharacterImage.GetComponent<Image>().sprite = Characters[num].GetComponent<Image>().sprite;
        }
        CharacterImage.GetComponent<AudioSource>().Play();
        MotionSetting();
    }

    public void MotionButton() {
        if (touchCounter < 5) {
            time = 0;
            touchCounter += 1;
            damage = true;
            CharacterMotion.GetComponent<AudioSource>().Play();
        }
        if (pause == false) {
            MotionSetting();    
        }
    }

    public void Reset() {
        if (time > 1.5f) { // ダメージを受けてから1.5f経ったら元のモーションに戻す
            time = 0;
            touchCounter = 0;
            damage = false;
        } 
    }
    
}
