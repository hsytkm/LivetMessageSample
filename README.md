# LivetMessageSample
LivetのMessageが表示されないことがあるの再現コード

## ソフト動作
MainWindowのEventTriggerにより、下記動作を行います。
1. ダイアログを表示する
2. MainWindow上のTextBlockの文字列を更新する

## 期待する結果
左クリック(PreviewMouseLeftButtonDown)とファイルドロップ(Drop)の各イベントで、上記動作が行われる。

## 実際の動作
左クリックでは1,2それぞれの動作が行われるが、ファイルドロップでは1の動作(ダイアログ表示)が行われないことがある。

ソフト起動後にDropのみを行った場合には、1が行われないが、
左クリックを行った後にDropを行うと、1が行われることがある。

## 解析結果
Livetを解析すると、Window.IsActiveがtrueでないと、MessageBoxを表示しないことが判明した。
Drop時はエクスプローラーなどのアプリにフォーカスが移っており、表示されていなかった。

Livet/Livet.Shared/Behaviors/Messaging/InteractionMessageAction.cs -> Invoke()

## 対策1
PreviewDragEnterイベント時に、WindowをActive化するBehaviorを作成して対応した。
Drop操作を行うと、本ソフトにフォーカス移る動作になっちゃったが、良しとする。

## 対策2
xamlで InformationDialogInteractionMessageAction に InvokeActionOnlyWhenWindowIsActive="False" を設定する。

本対応により、Window の Active状態に関わらず、ダイアログ（Messagebox）が表示されるようになる。

```xaml
<l:InformationDialogInteractionMessageAction InvokeActionOnlyWhenWindowIsActive="False" />
```

こちらは、okazuki様に教えていただきました。

twitterのつぶやきに対して的確なアドバイス頂き勉強になりました。大変感謝しております。

以上