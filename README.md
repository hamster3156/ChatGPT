# このリポジトリについて
神ゲー創造エボリューションのゲーム作品で、ChatGPTを利用してテキストを生成する機能を実装しました。このリポジトリでは作成したソースコードをまとめています。

# ダウロード方法
releaseからunitypackageをダウンロードしてください

# 必要なツール
・[UniTask](https://github.com/Cysharp/UniTask) \
・[Newtonsoft.Json-for-Unity](https://github.com/applejag/Newtonsoft.Json-for-Unity)　\
・[NaughtyAttributes](https://assetstore.unity.com/packages/tools/utilities/naughtyattributes-129996?locale=ja-JP) 

CNewtonsoft.Json-for-のUPMダウンロードについてですが、ドキュメントのUPMリンクの``com.unity.nuget.newtonsoft-json@3``でダウンロード出来なかったので、[[Unity]Newtonsoft.Jsonの基本的な使い方](https://qiita.com/kazuma_f/items/55a0b7ff628ab596e6ee)のUPMリンクでダウンロードしました。@3を抜けばダウンロードできるみたいです。
```C#
com.unity.nuget.newtonsoft-json
```

# 参考にした記事
・ChatGPTの送信処理\
[ChatGPT APIをUnityから動かす。](https://note.com/negipoyoc/n/n88189e590ac3)

・画像送信処理\
[GPT-4oをUnityで動かす](https://note.com/361yohen/n/n9d91a80002ab)

・Newtonsoft.Jsonの利用方法\
[Newtonsoft.Jsonの基本的な使い方](https://qiita.com/kazuma_f/items/55a0b7ff628ab596e6ee)

# 利用方法
GameObjectにSentenceGeneratorをアタッチして利用を行います。
![image](https://github.com/user-attachments/assets/f5b3a3f3-d8ff-4468-a9d6-1d04f1627d63)
テキスト生成と画像解析を利用するには、[OpenAI API](https://openai.com/index/openai-api/)サービスを利用してAPIキーを作成する必要があります。利用状況によって料金がかかるのでご注意ください。

APIキーを作成したらインスペクターに入力をします。\
![image](https://github.com/user-attachments/assets/229e7704-7798-434c-b528-07459de61164)

APIのモデルをチャット・画像解析共に変更出来ます。\
![image](https://github.com/user-attachments/assets/b8b1906e-2f02-46a1-ab44-35dac948cfaa)
![image](https://github.com/user-attachments/assets/70fd4bd1-178d-4fd1-bafe-57de3981ad2f) \
画像解析に関してですが、GPT-4oとGPT-4o-miniが現状対応していますが、画像解析はGPT-4oにした方が現状は良いと思います。

利用状況によって料金が~~と上で記述しましたがこの利用状況はトークンの使用量の合計によって判断されているので、メッセージや画像解析1回ごとの使用トークンが高ければ高いほど合計使用トークンが増え続けて利用料金が上がります。下の画像では各くモデルの使用トークンを表示しています。

GPT-4oの画像解析\
![image](https://github.com/user-attachments/assets/a4860626-83e9-4398-baa5-75114d727881)

GPT-4o-miniの画像解析\
![image](https://github.com/user-attachments/assets/6b9d1ada-ad5d-4c17-a803-99162e94c3a4)

比較をすると、GPT-4o-miniの使用トークンが4桁なのでGPT-4oの27倍近く多く使用していることが分かります。なので画像解析をする場合、現状はGPT-4oで行った方が良いと個人的には思います。

インスペクターから最大トークンを設定することができるので、利用するときに設定してください。\
![image](https://github.com/user-attachments/assets/dbeb6020-8c35-44b6-8214-7603e30839a5)

