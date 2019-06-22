CREATE TABLE [sys_ver] (
	[id] integer NOT NULL PRIMARY KEY AUTOINCREMENT, 
	[ver] integer NOT NULL, 
	[plugin] varchar(128) NOT NULL, 
	[create_time] datetime NOT NULL
);
INSERT INTO [sys_ver] (
	[ver]
	,[plugin]
	,[create_time]
	)
VALUES (
	2
	,'-'
	,date ('now')
	);

CREATE TABLE [sys_dict] (
	[id] integer NOT NULL PRIMARY KEY AUTOINCREMENT, 
	[od] int NOT NULL DEFAULT 0, 
	[cat] varchar(32) NOT NULL, 
	[key] varchar(16) NOT NULL, 
	[text] varchar(64) NOT NULL, 
	[tips] varchar(128), 
	[status] int NOT NULL DEFAULT 0, 
	[remark] varchar(256), 
	[create_time] datetime NOT NULL
);
INSERT INTO [sys_dict] ([od],[cat],[key],[text],[tips],[status],[remark],[create_time]) VALUES (0,'fms_method','1','不处理',NULL,1,NULL,'2019-06-01')
,(0,'fms_method','2','移动',NULL,1,NULL,'2019-06-01')
,(0,'fms_method','3','复制',NULL,1,NULL,'2019-06-01')
,(0,'fms_method','4','更名',NULL,1,NULL,'2019-06-01')
,(0,'fms_method','5','去重',NULL,1,NULL,'2019-06-01')
,(0,'fms_repeat','1','不处理',NULL,1,NULL,'2019-06-01')
,(0,'fms_repeat','2','覆盖',NULL,1,NULL,'2019-06-01')
,(0,'fms_repeat','3','追加',NULL,1,NULL,'2019-06-01')
,(0,'fms_repeat','4','去重',NULL,1,NULL,'2019-06-01');

CREATE TABLE [fms_rule] (
	[id] integer NOT NULL PRIMARY KEY AUTOINCREMENT, 
	[name] varchar(64), 
	[method] varchar(2) DEFAULT '0', 
	[src_file] varchar(2048), 
	[src_path] varchar(2048), 
	[dst_path] varchar(2048), 
	[dst_file] varchar(2048), 
	[repeat] int DEFAULT 0, 
	[remark] varchar(256), 
	[status] int DEFAULT 0, 
	[update_time] timestamp, 
	[create_time] datetime
);

CREATE TABLE [cat] (
	[id] integer NOT NULL PRIMARY KEY AUTOINCREMENT, 
	[pid] integer NOT NULL, 
	[names] varchar(256) NOT NULL, 
	[update_time] timestamp NOT NULL DEFAULT (CURRENT_TIMESTAMP), 
	[create_time] datetime NOT NULL DEFAULT (CURRENT_TIMESTAMP)
);

CREATE TABLE [cat_doc] (
	[id] integer NOT NULL PRIMARY KEY AUTOINCREMENT, 
	[doc_id] integer NOT NULL, 
	[cat_id] integer NOT NULL, 
	[update_time] timestamp NOT NULL DEFAULT (datetime('now','localtime')), 
	[create_time] datetime NOT NULL
);

CREATE UNIQUE INDEX [idx_cat_doc_cd]
	ON [cat_doc] ([cat_id], [doc_id]);

CREATE TABLE [doc] (
	[id] integer NOT NULL PRIMARY KEY AUTOINCREMENT, 
	[pid] integer NOT NULL DEFAULT 0, 
	[types] int NOT NULL DEFAULT 0, 
	[modes] int NOT NULL DEFAULT 0, 
	[names] varchar(128) NOT NULL DEFAULT '', 
	[path] varchar(1024) NOT NULL DEFAULT '', 
	[key] integer NOT NULL DEFAULT 0, 
	[remark] varchar(256), 
	[update_time] timestamp NOT NULL DEFAULT (CURRENT_TIMESTAMP), 
	[create_time] datetime NOT NULL DEFAULT (CURRENT_TIMESTAMP), 
	[file_type] varchar(16), 
	[file_date] varchar(16), 
	[file_time] varchar(8)
);

CREATE INDEX [idx_doc_ut]
	ON [doc] ([update_time]);

CREATE INDEX [idx_doc_ct]
	ON [doc] ([create_time]);

CREATE TABLE [doc_key] (
	[id] integer NOT NULL PRIMARY KEY AUTOINCREMENT, 
	[qty] int NOT NULL DEFAULT 0, 
	[key] varchar(256) NOT NULL, 
	[file] varchar(1024), 
	[create_time] datetime NOT NULL
);

CREATE UNIQUE INDEX [idx_doc_key]
	ON [doc_key] ([key]);

CREATE TABLE [doc_lnk] (
	[id] integer NOT NULL PRIMARY KEY AUTOINCREMENT, 
	[doc_id] integer NOT NULL, 
	[pid] integer NOT NULL, 
	[update_time] timestamp NOT NULL, 
	[create_time] datetime
);

CREATE INDEX [idx_doc_lnk_pid]
	ON [doc_lnk] ([pid]);

CREATE TABLE [tag] (
	[id] integer NOT NULL PRIMARY KEY AUTOINCREMENT, 
	[qty] integer NOT NULL DEFAULT 0, 
	[names] varchar(128) NOT NULL, 
	[update_time] timestamp NOT NULL DEFAULT (CURRENT_TIMESTAMP), 
	[create_time] datetime NOT NULL DEFAULT (CURRENT_TIMESTAMP)
);

CREATE TABLE [tag_doc] (
	[id] integer NOT NULL PRIMARY KEY AUTOINCREMENT, 
	[tag_id] integer NOT NULL, 
	[doc_id] integer NOT NULL, 
	[update_time] timestamp NOT NULL DEFAULT (datetime('now','localtime')), 
	[create_time] datetime NOT NULL
);

CREATE UNIQUE INDEX [idx_tag_doc_td]
	ON [tag_doc] ([tag_id], [doc_id]);

CREATE TABLE [cmd_file] (
	[id] integer NOT NULL PRIMARY KEY AUTOINCREMENT, 
	[od] int NOT NULL DEFAULT 0, 
	[os] varchar(8), 
	[text] varchar(1024) NOT NULL, 
	[tips] varchar(256), 
	[file] varchar(256), 
	[path] varchar(1024) NOT NULL, 
	[keys] varchar(256), 
	[status] int NOT NULL DEFAULT 0, 
	[create_time] datetime NOT NULL, 
	[update_time] timestamp NOT NULL
);