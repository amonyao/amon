/*数据库版本*/
CREATE TABLE [ver] ([id] INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT, [ver] INTEGER NOT NULL, [create_time] DATETIME NOT NULL)

/*文档实体类*/
CREATE TABLE [doc] ([id] integer NOT NULL PRIMARY KEY AUTOINCREMENT, [pid] integer NOT NULL DEFAULT 0, [types] int NOT NULL DEFAULT 0, [modes] int NOT NULL DEFAULT 0, [name] varchar(128) NOT NULL DEFAULT '', [path] varchar(1024) NOT NULL DEFAULT '', [file] varchar(32) NOT NULL DEFAULT '', [remark] varchar(256), [file_type] varchar(16), 	[file_date] varchar(16), 	[file_time] varchar(8), 	[update_time] timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP, 	[create_time] datetime NOT NULL DEFAULT CURRENT_TIMESTAMP);

CREATE INDEX [idx_doc_mn] ON [doc] ([modes], [name])
CREATE INDEX [idx_doc_ut] ON [doc] ([update_time])
CREATE INDEX [idx_doc_ct] ON [doc] ([create_time])

/*目录实体类*/
CREATE TABLE [cat] ([id] integer NOT NULL PRIMARY KEY AUTOINCREMENT, [pid] integer NOT NULL, [names] varchar(256) NOT NULL, [update_time] timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP, [create_time] datetime)
CREATE INDEX [idx_cat_names] ON [cat] ([names])

CREATE TABLE [cat_doc] ([id] integer NOT NULL PRIMARY KEY AUTOINCREMENT, [doc_id] integer NOT NULL, [cat_id] integer NOT NULL, [update_time] timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP, [create_time] datetime NOT NULL)
CREATE UNIQUE INDEX [idx_cat_doc_cd] ON [cat_doc] ([cat_id], [doc_id])

CREATE TABLE [doc_key] ([id] integer NOT NULL PRIMARY KEY, [key] varchar(256) NOT NULL, [create_time] datetime NOT NULL)

CREATE TABLE [doc_lnk] ([id] integer NOT NULL PRIMARY KEY AUTOINCREMENT, [doc_id] integer NOT NULL, [pid] integer NOT NULL, [update_time] timestamp NOT NULL, [create_time] datetime)
CREATE INDEX [idx_doc_lnk_pid] ON [doc_lnk] ([pid])

/*文档标签类*/
CREATE TABLE [tag] ([id] integer NOT NULL PRIMARY KEY AUTOINCREMENT, [qty] integer NOT NULL, [names] varchar(128) NOT NULL, [update_time] timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP, [create_time] datetime NOT NULL)
CREATE UNIQUE INDEX [idx_tag_names] ON [tag]([names])

CREATE TABLE [tag_doc] ([id] integer NOT NULL PRIMARY KEY AUTOINCREMENT, [tag_id] integer NOT NULL, [doc_id] integer NOT NULL, [update_time] timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP, [create_time] datetime NOT NULL)
CREATE UNIQUE INDEX [idx_tag_doc_td] ON [tag_doc] ([tag_id], [doc_id])

/*数据初始化*/
INSERT INTO [ver] ([ver],[create_time]) VALUES (1,date('now'))