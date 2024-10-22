# このリポジトリについて
神ゲー創造エボリューションのゲーム作品で、ChatGPTを利用してテキストを生成する機能を実装しました。このリポジトリでは作成したソースコードをまとめています。

# ダウロード方法
releaseからunitypackageをダウンロードしてください

# 必要なツール
・[UniTask](https://github.com/Cysharp/UniTask) \
・[Newtonsoft.Json-for-Unity](https://github.com/applejag/Newtonsoft.Json-for-Unity)　\
・[NaughtyAttributes](https://assetstore.unity.com/packages/tools/utilities/naughtyattributes-129996?locale=ja-JP) 

Newtonsoft.Json-for-のUPMダウンロードについてですが、ドキュメントのUPMリンクの``com.unity.nuget.newtonsoft-json@3``でダウンロード出来なかったので、[[Unity]Newtonsoft.Jsonの基本的な使い方](https://qiita.com/kazuma_f/items/55a0b7ff628ab596e6ee)のUPMリンクでダウンロードしました。@3を抜けばダウンロードできるみたいです。
```C#
com.unity.nuget.newtonsoft-json
```

# 参考にした記事
・[ChatGPT APIをUnityから動かす。](https://note.com/negipoyoc/n/n88189e590ac3) \
・[GPT-4oをUnityで動かす](https://note.com/361yohen/n/n9d91a80002ab) \
・[Newtonsoft.Jsonの基本的な使い方](https://qiita.com/kazuma_f/items/55a0b7ff628ab596e6ee)

# 利用方法
・GameObjectにSentenceGeneratorをアタッチして利用を行います。
![image](https://github.com/user-attachments/assets/f5b3a3f3-d8ff-4468-a9d6-1d04f1627d63)
テキスト生成と画像解析を利用するには、[OpenAI API](https://openai.com/index/openai-api/)サービスを利用してAPIキーを作成する必要があります。利用状況によって料金がかかるのでご注意ください。

・APIキーを作成したらインスペクターに入力をします。\
![image](https://github.com/user-attachments/assets/229e7704-7798-434c-b528-07459de61164)

・GPTの返答方法を設定します。\
![image](https://github.com/user-attachments/assets/c4cf8bdd-64b7-4fa7-8ec1-176b291570a9)

・APIのモデルはチャット・画像解析共に変更出来ます。\
![image](https://github.com/user-attachments/assets/b8b1906e-2f02-46a1-ab44-35dac948cfaa)
![image](https://github.com/user-attachments/assets/70fd4bd1-178d-4fd1-bafe-57de3981ad2f) \

・インスペクターから最大使用トークンを設定できます。\
![image](https://github.com/user-attachments/assets/dbeb6020-8c35-44b6-8214-7603e30839a5)

・デバックログを出すフラグです。\
![image](https://github.com/user-attachments/assets/21d55d4b-5bb0-43f9-be5f-22fb7a1298a4)

