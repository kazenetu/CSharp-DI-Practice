# CSharp-DI-Practice
コンソールアプリとクラスライブラリ(ビジネスロジック)でDIの試作

## 実行環境
* .NET8SDK

## dotnetコマンド
本ドキュメントで下記コマンドを発行  
```sh
dotnet run --project ConsoleApp/ConsoleApp.csproj
```

### 実行結果
**日時はダミー**
* コンソールログ  
  ```log
    [2026/01/01 00:00:00] info: ConsoleApp.App[0]
        ---JSONFile---
        1:あいうえお
        2:かきくけこ
        3:さしすせそ
        4:
        5:行飛ばし
        
    [2026/01/01 00:00:00] info: ConsoleApp.App[0]
        ---XMLFile---
        １行目：あいうえお
        ２行目：かきくけこ
        ３行目：さしすせそ
        
        ５行目：行飛ばし
        
    [2026/01/01 00:00:00] info: ConsoleApp.App[0]
        ---CSVFile---
        1:あいうえお
        2:かきくけこ
        3:さしすせそ
        4:
        5:行飛ばし
  ```

