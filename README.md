>《7天學會大數據資料處理NoSQL》ISBN:9789864343942
<https://github.com/taipeitechmmslab/MMSLAB-MongoDB>

<span id="contents"> </span>
MongoDB
====
<a href="#contents">

Contents
* [2.4 檢查與啟動MongoDB服務](#contents)
* [3.3 基本操作](#3_3)
* [7. 索引與效能分析](#ch7)


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

### 5.1, 6.1 資料庫基本操作
#### 查詢運算子
◎關聯式資料庫
```SQL
SELECT 書本名稱, 價錢
FROM library
WHERE 借閱人="王小明"
```
◎MongoDB
```json
db.library.find({借閱人:"王小明"}, {書本名稱:true, 價錢:true})
```
#### 插入操作
◎關聯式資料庫
```sql
INSERT INTO 書籍借閱紀錄 (編號, 書本名稱, 價錢, 借閱人, 借閱時間)
VALUE (4, "超人氣銅板美食", 250, "小華", "2015/7/30 22:30")
```
◎MongoDB
```json
db.書籍借閱紀錄.insert({
    編號:4,
    書本名稱:"超人氣銅板美食",
    價錢:250,
    借閱人:"小華",
    借閱時間:ISODate("2015-07-30T22:30:30:00Z")
})
```
#### 更新操作
◎關聯式資料庫
```sql
UPDATE library
SET 借閱人="王大陸"
WHERE 書本名稱="實用英文會話"
```
◎MongoDB
```json
db.library.update({書本名稱:"實用英文會話"}, {$set:{借閱人:"王大陸"}})
```
#### 刪除操作
◎關聯式資料庫
```sql
DELETE
FROM library
WHERE 借閱人="阿能"
```
◎MongoDB
```json
db.library.remove({借閱人:"阿能"})
```

<span id="ch7"></span>
### 7. 索引與效能分析

#### 7.1.1 索引(Indexes)
MongoDB在查詢資料時，會做兩個動作，分別為遍歷或掃描(Scan)、取得資料(Retrieve)；遍歷的動作是非常耗時的，因此在資料量非常攏大的時候，將資料對特定條件設定索引(Indexs)，來限制遍歷資料的數量，並減少了取得資料的時間，因此能增快查詢的效率。在新增索引的同時也可對鍵值或屬性(Key)進行相關(Value)的排序，因此增快了搜索效率。MongoDB提供了六種[索引方法](https://docs.mongodb.com/manual/indexes/)：

1. [單一欄位(Single Field)](https://docs.mongodb.com/manual/core/index-single/)
2. [組合索引(Compound Indexes)](https://docs.mongodb.com/manual/core/index-compound/)
3. [文字索引(Text Indexes)](https://docs.mongodb.com/manual/core/index-text/)
4. [多重鍵值索引(Multikey Indexes)](https://docs.mongodb.com/manual/core/index-multikey/)
5. [地理空間索引(Geospatial Indexes)](https://docs.mongodb.com/manual/core/2dsphere/)
6. [雜湊索引(Hashed Indexes)](https://docs.mongodb.com/manual/core/index-hashed/)
```json
// 建立單一欄位索引
db.students.createIndex({score:1})
//=== 執行搜索操作
db.students.find(
    {score:{ $gte:50, $lte:80 }}
) //搜索students集合score大於等於50，小於等於80
//此指令需要切換到"db.system.profile"集合內找尋紀錄

db.students.find(
    {score:{ $gte:50, $lte:80 }}
).explain("executionStats")
//直接將執行記錄寫在搜尋結果(bson object)，該記錄寫在建立類別"executionStats"裡
```
透過`db.students.createIndex({score:1})`，在students集合建立一個score欄位<span style="background-color:yellow">遞增</span>排序的索引，而`db.students.createIndex({score:-1})`為<span style="background-color:yellow">遞減</span>排序索引。

#### 7.1.2 查詢計畫(Query Plan)
我們可以透過查詢計畫，來判斷目前的查詢是否有使用索引，以及在查詢時索引的使用效率。

■ 設定此資料的分析等級：
```json
db.setProfilingLevel(<#等級>)

db.setProfilingLevel(0) //不收集任何資訊
db.setProfilingLevel(1, {slowms: 20}) //大於20微秒
db.setProfilingLevel(2) //收集所有操作資訊

db.getProfilingStatus() //可查詢設定狀態
```
|等級|描述|
|---|---|
|0|預設等級，不收集任何資料|
|1|收集操作的時間大於slowms|
|2|收集所有資料|

■ 查詢結果
在collections目錄上按右鍵選擇`Refresh`來更新集合；當出現`system.profile`集合，代表db.setProfilingLevel(2)操作成功。