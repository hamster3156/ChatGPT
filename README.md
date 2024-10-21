# このリポジトリについて
神ゲー創造エボリューションのゲーム作品で、ChatGPTを利用してテキストを生成する機能を実装しました。このリポジトリでは作成したソースコードをまとめています。

# ダウロード方法
releaseからunitypackageをダウンロードしてください

# 必要なツール

・[UniTask](https://github.com/Cysharp/UniTask) \
テキストを生成する処理で非同期処理で利用しています。

・[Newtonsoft.Json-for-Unity](https://github.com/applejag/Newtonsoft.Json-for-Unity)　\
ChatGPTに送信する情報をJson形式に変換するためにNewtonsoft.Json-for-も必要です。ドキュメントのUPMでダウンロード出来なかったので、[kazuma_fさん](https://qiita.com/kazuma_f)が公開している[記事](https://qiita.com/kazuma_f/items/55a0b7ff628ab596e6ee)のUPMでダウンロードしました。
```C#
com.unity.nuget.newtonsoft-json
```
また、[公式のオンラインドキュメント](https://github.com/applejag/Newtonsoft.Json-for-Unity/wiki/Install-official-via-UPM)で、上の方法でUPMパッケージをダウンロードする記述を見つけたので...入れ方としてはこれが正しいかも...?ステップ4辺りに書いてあります。

・[NaughtyAttributes](https://assetstore.unity.com/packages/tools/utilities/naughtyattributes-129996?locale=ja-JP) \
エディター上からプロンプトの初期化メソッドを実行するためにNaughtyAttributesアセットを利用してボタンを表示するという部分で利用しています。

# 参考にした記事
・ChatGPTの送信処理\
[ねぎぽよしさん](https://note.com/negipoyoc)の[この記事](https://note.com/negipoyoc/n/n88189e590ac3)を元に作成しています。

・画像送信処理\
[よーへんさん](https://note.com/361yohen)の[この記事を](https://note.com/361yohen/n/n9d91a80002ab)参考にしています。
