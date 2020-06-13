>《7天學會大數據資料處理NoSQL》ISBN:9789864343942
<https://github.com/taipeitechmmslab/MMSLAB-MongoDB>

MongoDB
====
<span id="contents"><a href="#contents"> </span>
Contents
* [2.4 檢查與啟動MongoDB服務](#2_4)
* [3.3 基本操作](#3_3)


<span id="2_4"></span>
### 2.4 檢查與啟動MongoDB服務
1. 方法1：使用Windows Service啟動MongoDB服務
安裝MongoDB至Service，首先使用管理員開啟cmd：
```shell
mongod --config "D:\Program_Files\MongoDB\Server\4.2\bin\mongod.cfg" -install
'''
其實在安裝MongoDB過程中已經安裝了一個MongoDB服務，
我們只需要檢查電腦上的MongoDB服務狀態，
上述指令是教你如何新增服務至Windows Service
"mongod.cfg"為設定檔，裡面設定了資料儲存位置、日誌紀錄位置與網路設定。"
'''
#----啟動/停止
net start mongodb #啟動
net stop mongodb #停止

#----移除Windows Service服務(在安裝好服務後執行)
mongod --remove
```

在查詢欄位輸入「服務」，確認MongoDB服務狀態，用滑鼠點擊視窗來啟動/停止服務。
![](./img/2_4service.png)

<span id="3_3"></span>
### 3.3 基本操作
要操作資料庫都要先進入mongo shell，在cmd裡輸入`mongo`
```shell
show dbs #可顯示目前所有的資料庫
use ntut #使用ntut的資料庫
db #檢查目前使用的資料庫名稱
db.students.insert({}) #在以students集合插入一筆空資料
show collections #可檢查db內的集合數量
db.students.count() #可檢查db資料庫內students集合的數量
#---刪除
db.dropDatabase() #刪除目前的資料庫
db.students.drop() #刪除以students的集合
```
表3-1 官方提供的`cmd`執行列表
|指令|用途說明|
|---|---|
|mongo|開啟操作shell|
|mongostat|顯示目前運行的MongoDB資料庫每一秒的狀態，可以加上參數來每幾秒運行一次|
|mongotop|顯示目前運行的MongoDB資料庫的每一個集合(collection)每一秒的執行操作花費時間|
|mongodump|輸出二進制的資料庫內容檔案，以進行資料備份|
|mongorestore|讀取二進制的資料庫內容檔案，進行資料還原|