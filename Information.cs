using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Information : MonoBehaviour
{
    public int num; // 情報の割り当て番号
    public AudioClip clip; // 曲情報
    public string jpn_name; // キャラクター日本語表記
    public string title; // 曲名情報
    public string jpn_title; // 曲名日本語表記
    public Sprite jacket; // レコードジャケットの色
    public Sprite image; // キャラクターの情報
    public Sprite[] images; // キャラクターのポーズ
    public Sprite damage; // キャラクターのダメージモーション
    public Sprite lose; // キャラクターの負けモーション
    public Sprite image2; // 2Pカラーのキャラクター情報
    public Sprite[] images2; // 2Pカラーのキャラクターポーズ
    public Sprite damage2; // 2Pカラーのダメージモーション
    public Sprite lose2; // 2Pカラーの負けモーション
    
    
    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
