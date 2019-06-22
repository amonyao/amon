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
INSERT INTO [cmd_file] ([od],[os],[text],[tips],[file],[path],[keys],[status],[create_time],[update_time]) VALUES (0,'win','记事本','记事本','notepad','notepad.exe','notepad',1,'2019-06-18','2019-06-22 21:15:52.1343263')
INSERT INTO [cmd_file] ([od],[os],[text],[tips],[file],[path],[keys],[status],[create_time],[update_time]) VALUES (1,'win','命令行','命令行','cmd','cmd.exe','cmd',1,'2019-06-18','2019-06-22 21:15:57.3191225')
INSERT INTO [cmd_file] ([od],[os],[text],[tips],[file],[path],[keys],[status],[create_time],[update_time]) VALUES (0,'win','写字板','写字板','write','write.exe','write',1,'2019-06-18','2019-06-22 21:22:53.2371846')
INSERT INTO [cmd_file] ([od],[os],[text],[tips],[file],[path],[keys],[status],[create_time],[update_time]) VALUES (0,NULL,'注册表编辑器','注册表编辑器','regedit','regedit.exe','regedit,zhucebiao',1,'2019-06-22 20:30:31.9357829','2019-06-22 20:30:31.9357829')
INSERT INTO [cmd_file] ([od],[os],[text],[tips],[file],[path],[keys],[status],[create_time],[update_time]) VALUES (0,NULL,'资源管理器','资源管理器','explorer','explorer.exe','explorer',1,'2019-06-22 20:31:05.2157529','2019-06-22 20:31:05.2157529')
INSERT INTO [cmd_file] ([od],[os],[text],[tips],[file],[path],[keys],[status],[create_time],[update_time]) VALUES (0,NULL,'系统配置','系统配置','msconfig','msconfig.exe','msconfig,xitongpeizhi',1,'2019-06-22 21:23:41.9507684','2019-06-22 21:23:41.9507684')
INSERT INTO [cmd_file] ([od],[os],[text],[tips],[file],[path],[keys],[status],[create_time],[update_time]) VALUES (0,NULL,'计算器','计算器','calc','calc.exe','calc,jisuanqi',1,'2019-06-22 21:24:00.7123521','2019-06-22 21:24:00.7123521')
INSERT INTO [cmd_file] ([od],[os],[text],[tips],[file],[path],[keys],[status],[create_time],[update_time]) VALUES (0,NULL,'远程桌面','远程桌面','mstsc','mstsc.exe','mstsc,yuanchengzhuomian',1,'2019-06-22 21:25:13.1272232','2019-06-22 21:25:13.1272232')
INSERT INTO [cmd_file] ([od],[os],[text],[tips],[file],[path],[keys],[status],[create_time],[update_time]) VALUES (0,NULL,'系统服务','系统服务','services','services.msc','services,xitongfuwu',1,'2019-06-22 21:25:33.4902429','2019-06-22 21:25:33.4902429')
INSERT INTO [cmd_file] ([od],[os],[text],[tips],[file],[path],[keys],[status],[create_time],[update_time]) VALUES (0,NULL,'组策略','组策略','gpedit','gpedit.msc','gpedit,zucelve,zucelue',1,'2019-06-22 21:25:56.4594100','2019-06-22 21:25:56.4594100')
INSERT INTO [cmd_file] ([od],[os],[text],[tips],[file],[path],[keys],[status],[create_time],[update_time]) VALUES (0,NULL,'组件服务','组件服务','dcomcnfg','dcomcnfg.exe','dcomcnfg,zujianfuwu',1,'2019-06-22 21:26:15.9857078','2019-06-22 21:26:15.9857078')
INSERT INTO [cmd_file] ([od],[os],[text],[tips],[file],[path],[keys],[status],[create_time],[update_time]) VALUES (0,NULL,'设备管理器','设备管理器','devmgmt','devmgmt.msc','devmgmt,shebeiguanliqi',1,'2019-06-22 21:26:33.0557938','2019-06-22 21:26:33.0557938')
INSERT INTO [cmd_file] ([od],[os],[text],[tips],[file],[path],[keys],[status],[create_time],[update_time]) VALUES (0,NULL,'磁盘清理','磁盘清理','cleanmgr','cleanmgr.exe','cleanmgr,cipanqingli',1,'2019-06-22 21:26:49.4061655','2019-06-22 21:26:49.4061655')
INSERT INTO [cmd_file] ([od],[os],[text],[tips],[file],[path],[keys],[status],[create_time],[update_time]) VALUES (0,NULL,'计算机管理','计算机管理','compmgmt','compmgmt.msc','compmgmt,jisuanjiguanli',1,'2019-06-22 21:27:06.8709647','2019-06-22 21:27:06.8709647')
INSERT INTO [cmd_file] ([od],[os],[text],[tips],[file],[path],[keys],[status],[create_time],[update_time]) VALUES (0,NULL,'本地安全策略','本地安全策略','secpol','secpol.msc','secpol,bendianquancelue',1,'2019-06-22 21:27:23.7569974','2019-06-22 21:27:23.7569974')
INSERT INTO [cmd_file] ([od],[os],[text],[tips],[file],[path],[keys],[status],[create_time],[update_time]) VALUES (0,NULL,'任务管理器','任务管理器','taskmgr','taskmgr.exe','taskmgr,renweuguanliqi',1,'2019-06-22 21:27:41.4272919','2019-06-22 21:27:41.4272919')
INSERT INTO [cmd_file] ([od],[os],[text],[tips],[file],[path],[keys],[status],[create_time],[update_time]) VALUES (0,NULL,'Microsoft 管理控制台','Microsoft 管理控制台','mmc','mmc.exe','mmc,guanlikongzhitai',1,'2019-06-22 21:28:01.5536638','2019-06-22 21:28:01.5536638')
INSERT INTO [cmd_file] ([od],[os],[text],[tips],[file],[path],[keys],[status],[create_time],[update_time]) VALUES (0,NULL,'本地用户和组','本地用户和组','lusrmgr','lusrmgr.msc','lusrmgr,bendiyonghuhezu',1,'2019-06-22 21:28:20.5656617','2019-06-22 21:28:20.5656617')
INSERT INTO [cmd_file] ([od],[os],[text],[tips],[file],[path],[keys],[status],[create_time],[update_time]) VALUES (0,NULL,'DVD播放器','DVD播放器','dvdplay','dvdplay.exe','dvdplay,dvdbofangqi',1,'2019-06-22 21:30:03.7433762','2019-06-22 21:30:03.7433762')
INSERT INTO [cmd_file] ([od],[os],[text],[tips],[file],[path],[keys],[status],[create_time],[update_time]) VALUES (0,NULL,'磁盘管理','磁盘管理','diskmgmt','diskmgmt.msc','diskmgmt,cipanguanli',1,'2019-06-22 21:31:07.1847027','2019-06-22 21:31:07.1847027')
INSERT INTO [cmd_file] ([od],[os],[text],[tips],[file],[path],[keys],[status],[create_time],[update_time]) VALUES (0,NULL,'DirectX诊断工具','DirectX诊断工具','dxdiag','dxdiag.exe','dxdiag,directx',1,'2019-06-22 21:31:44.2340629','2019-06-22 21:31:44.2340629')
INSERT INTO [cmd_file] ([od],[os],[text],[tips],[file],[path],[keys],[status],[create_time],[update_time]) VALUES (0,NULL,'性能监视器','性能监视器','perfmon','perfmon.msc','perfmon,xingnengjianshiqi',1,'2019-06-22 21:32:02.9168971','2019-06-22 21:32:02.9168971')
INSERT INTO [cmd_file] ([od],[os],[text],[tips],[file],[path],[keys],[status],[create_time],[update_time]) VALUES (0,NULL,'关于Windows','关于Windows','winver','winver.exe','winver,windows',1,'2019-06-22 21:32:20.2854249','2019-06-22 21:32:20.2854249')
INSERT INTO [cmd_file] ([od],[os],[text],[tips],[file],[path],[keys],[status],[create_time],[update_time]) VALUES (0,NULL,'画图','画图','mspaint','mspaint.exe','mspaint,huatu',1,'2019-06-22 21:32:38.2051278','2019-06-22 21:32:38.2051278')
INSERT INTO [cmd_file] ([od],[os],[text],[tips],[file],[path],[keys],[status],[create_time],[update_time]) VALUES (0,NULL,'放大镜','放大镜','magnify','magnify.exe','magnify,fangdajing',1,'2019-06-22 21:32:54.1908573','2019-06-22 21:32:54.1908573')
INSERT INTO [cmd_file] ([od],[os],[text],[tips],[file],[path],[keys],[status],[create_time],[update_time]) VALUES (0,NULL,'屏幕键盘','屏幕键盘','osk','osk.exe','osk,pingmujianpan',1,'2019-06-22 21:33:15.5525230','2019-06-22 21:33:15.5525230')
INSERT INTO [cmd_file] ([od],[os],[text],[tips],[file],[path],[keys],[status],[create_time],[update_time]) VALUES (0,NULL,'ODBC数据源管理程序','ODBC数据源管理程序','odbcad32','odbcad32.exe','odbcad32,odbcshujuyuanguanli',1,'2019-06-22 21:33:44.9514909','2019-06-22 21:33:44.9514909')
INSERT INTO [cmd_file] ([od],[os],[text],[tips],[file],[path],[keys],[status],[create_time],[update_time]) VALUES (0,NULL,'音量控制','音量控制','sndvol','sndvol.exe','sndvol,yinliangkongzhi',1,'2019-06-22 21:34:00.8410911','2019-06-22 21:34:00.8410911')
INSERT INTO [cmd_file] ([od],[os],[text],[tips],[file],[path],[keys],[status],[create_time],[update_time]) VALUES (0,NULL,'事件查看器','事件查看器','eventvwr','eventvwr.exe','eventvwr,shijianchakanqi',1,'2019-06-22 21:34:23.9435750','2019-06-22 21:34:23.9435750')
INSERT INTO [cmd_file] ([od],[os],[text],[tips],[file],[path],[keys],[status],[create_time],[update_time]) VALUES (0,NULL,'证书管理','证书管理','certmgr','certmgr.msc','certmgr,zhengshuguanli',1,'2019-06-22 21:34:40.7829870','2019-06-22 21:34:40.7829870')
INSERT INTO [cmd_file] ([od],[os],[text],[tips],[file],[path],[keys],[status],[create_time],[update_time]) VALUES (0,NULL,'组策略','组策略','gpedit','gpedit.msc','gpedit,zucelue',1,'2019-06-22 21:34:57.4283645','2019-06-22 21:34:57.4283645')
INSERT INTO [cmd_file] ([od],[os],[text],[tips],[file],[path],[keys],[status],[create_time],[update_time]) VALUES (0,NULL,'字符映射表','字符映射表','charmap','charmap.exe','charmap,zifuyingshe',1,'2019-06-22 21:35:14.8211022','2019-06-22 21:35:14.8211022')
INSERT INTO [cmd_file] ([od],[os],[text],[tips],[file],[path],[keys],[status],[create_time],[update_time]) VALUES (0,'win10','程序和功能','程序和功能','appwiz','appwiz.cpl','appwiz,chenxuhegongneng',1,'2019-06-22 21:35:30.1618310','2019-06-22 21:35:30.1618310')
INSERT INTO [cmd_file] ([od],[os],[text],[tips],[file],[path],[keys],[status],[create_time],[update_time]) VALUES (0,NULL,'讲述人','讲述人','narrator','narrator.exe','narrator,jiangshuren',1,'2019-06-22 21:35:45.8410654','2019-06-22 21:35:45.8410654')
INSERT INTO [cmd_file] ([od],[os],[text],[tips],[file],[path],[keys],[status],[create_time],[update_time]) VALUES (0,NULL,'颜色管理','配置显示器和打印机等中的色彩','colorcpl','colorcpl.exe','colorcpl,yanseguanli',1,'2019-06-22 21:43:11.8190177','2019-06-22 21:43:11.8190177')
INSERT INTO [cmd_file] ([od],[os],[text],[tips],[file],[path],[keys],[status],[create_time],[update_time]) VALUES (0,NULL,'计算机管理','计算机管理','CompMgmtLauncher','CompMgmtLauncher.exe','CompMgmtLauncher,jisuanjiguanli',1,'2019-06-22 21:44:05.1874013','2019-06-22 21:44:05.1874013')
INSERT INTO [cmd_file] ([od],[os],[text],[tips],[file],[path],[keys],[status],[create_time],[update_time]) VALUES (0,NULL,'组件服务','组件服务','comexp','comexp.msc','comexp,zujianfuwu',1,'2019-06-22 21:45:11.2309785','2019-06-22 21:45:11.2309785')
INSERT INTO [cmd_file] ([od],[os],[text],[tips],[file],[path],[keys],[status],[create_time],[update_time]) VALUES (0,NULL,'控制面版','控制面版','control','control.exe','control,kongzhimianban',1,'2019-06-22 21:45:43.5144629','2019-06-22 21:45:43.5144629')
INSERT INTO [cmd_file] ([od],[os],[text],[tips],[file],[path],[keys],[status],[create_time],[update_time]) VALUES (0,NULL,'颜色校准','显示颜色校准','dccw','dccw.exe','dccw,yansejiaozhun',1,'2019-06-22 21:46:31.2122854','2019-06-22 21:46:31.2122854')
INSERT INTO [cmd_file] ([od],[os],[text],[tips],[file],[path],[keys],[status],[create_time],[update_time]) VALUES (0,NULL,'屏幕分辨率','屏幕分辨率','desk','desk.cpl','desk,pingmufenbianlv',1,'2019-06-22 21:47:10.3753432','2019-06-22 21:47:10.3753432')
INSERT INTO [cmd_file] ([od],[os],[text],[tips],[file],[path],[keys],[status],[create_time],[update_time]) VALUES (0,NULL,'电话拨号程序','电话拨号程序','dialer','dialer.exe','dialer,dianhuabohaochenglu',1,'2019-06-22 21:47:49.6010300','2019-06-22 21:47:49.6010300')
INSERT INTO [cmd_file] ([od],[os],[text],[tips],[file],[path],[keys],[status],[create_time],[update_time]) VALUES (0,NULL,'造字程序','造字程序','eudcedit','eudcedit.exe','eudcedit,zaozichengxu',1,'2019-06-22 21:48:36.1491577','2019-06-22 21:48:36.1491577')
INSERT INTO [cmd_file] ([od],[os],[text],[tips],[file],[path],[keys],[status],[create_time],[update_time]) VALUES (0,NULL,'Windows防火墙','Windows防火墙','firewall','firewall.cpl','firewall,fanghuoqiang',1,'2019-06-22 21:49:30.8332318','2019-06-22 21:49:30.8332318')
INSERT INTO [cmd_file] ([od],[os],[text],[tips],[file],[path],[keys],[status],[create_time],[update_time]) VALUES (0,NULL,'传真封面编辑器','传真封面编辑器','fxscover','fxscover.exe','fxscover,chuanzhenfengmianbianji',1,'2019-06-22 21:50:26.8634182','2019-06-22 21:50:26.8634182')
INSERT INTO [cmd_file] ([od],[os],[text],[tips],[file],[path],[keys],[status],[create_time],[update_time]) VALUES (0,NULL,'共享文件夹管理器','共享文件夹管理器','fsmgmt','fsmgmt.msc','fsmgmt,gongxiangwenjianjiaguanliqi',1,'2019-06-22 21:51:04.0348773','2019-06-22 21:51:04.0348773')
INSERT INTO [cmd_file] ([od],[os],[text],[tips],[file],[path],[keys],[status],[create_time],[update_time]) VALUES (0,NULL,'设备管理器','设备管理器','hdwwiz','hdwwiz.cpl','hdwwiz,shebeiguanliqi',1,'2019-06-22 21:51:46.6343542','2019-06-22 21:51:46.6343542')
INSERT INTO [cmd_file] ([od],[os],[text],[tips],[file],[path],[keys],[status],[create_time],[update_time]) VALUES (0,NULL,'Internet属性','Internet属性','inetcpl','inetcpl.cpl','inetcpl,internetshuxing',1,'2019-06-22 21:52:17.9939823','2019-06-22 21:52:17.9939823')
INSERT INTO [cmd_file] ([od],[os],[text],[tips],[file],[path],[keys],[status],[create_time],[update_time]) VALUES (0,NULL,'区域','操作系统区域管理','intl','intl.cpl','intl,quyu',1,'2019-06-22 21:52:54.8725894','2019-06-22 21:52:54.8725894')
INSERT INTO [cmd_file] ([od],[os],[text],[tips],[file],[path],[keys],[status],[create_time],[update_time]) VALUES (0,NULL,'游戏控制器','游戏控制器','joy','joy.cpl','joy,youxikongzhiqi',1,'2019-06-22 21:53:26.3180635','2019-06-22 21:53:26.3180635')
INSERT INTO [cmd_file] ([od],[os],[text],[tips],[file],[path],[keys],[status],[create_time],[update_time]) VALUES (0,NULL,'安装或卸载显示语言','安装或卸载显示语言','lpksetup','lpksetup.exe','lpksetup,yuyanbaoanzhuang',1,'2019-06-22 21:54:35.3820886','2019-06-22 21:54:35.3820886')
INSERT INTO [cmd_file] ([od],[os],[text],[tips],[file],[path],[keys],[status],[create_time],[update_time]) VALUES (0,NULL,'鼠标属性','鼠标属性','main','main.cpl','main,shubiaoshuxing',1,'2019-06-22 21:55:13.9929789','2019-06-22 21:55:13.9929789')
INSERT INTO [cmd_file] ([od],[os],[text],[tips],[file],[path],[keys],[status],[create_time],[update_time]) VALUES (0,NULL,'声音','声音','mmsys','mmsys.cpl','mmsys,shengyin',1,'2019-06-22 21:55:45.8213651','2019-06-22 21:55:45.8213651')
INSERT INTO [cmd_file] ([od],[os],[text],[tips],[file],[path],[keys],[status],[create_time],[update_time]) VALUES (0,NULL,'Windows内存诊断','Windows内存诊断','mdsched','mdsched.exe','mdsched,neicunzhenduan',1,'2019-06-22 21:57:00.8377212','2019-06-22 21:57:00.8377212')
INSERT INTO [cmd_file] ([od],[os],[text],[tips],[file],[path],[keys],[status],[create_time],[update_time]) VALUES (0,NULL,'同步中心','同步中心','mobsync','mobsync.exe','mobsync,tongbuzhongxin',1,'2019-06-22 21:57:37.4207201','2019-06-22 21:57:37.4207201')
INSERT INTO [cmd_file] ([od],[os],[text],[tips],[file],[path],[keys],[status],[create_time],[update_time]) VALUES (0,NULL,'Windows Media Player','Windows Media Player','mplayer2','mplayer2.exe','mplayer2,bofangqi',1,'2019-06-22 21:58:20.2838744','2019-06-22 21:58:20.2838744')
INSERT INTO [cmd_file] ([od],[os],[text],[tips],[file],[path],[keys],[status],[create_time],[update_time]) VALUES (0,NULL,'Microsoft支持诊断工具','Microsoft支持诊断工具','msdt','msdt.exe','msdt,zhenduangongju',1,'2019-06-22 21:59:01.4300281','2019-06-22 21:59:01.4300281')
INSERT INTO [cmd_file] ([od],[os],[text],[tips],[file],[path],[keys],[status],[create_time],[update_time]) VALUES (0,NULL,'系统信息','系统信息','msinfo32','msinfo32.exe','msinfo32,xitongxinxi',1,'2019-06-22 21:59:36.5132997','2019-06-22 21:59:36.5132997')
INSERT INTO [cmd_file] ([od],[os],[text],[tips],[file],[path],[keys],[status],[create_time],[update_time]) VALUES (0,NULL,'Windows远程协助','Windows远程协助','msra','msra.exe','msra,yuanchengxiezhu',1,'2019-06-22 22:00:20.4386707','2019-06-22 22:00:31.5903446')
INSERT INTO [cmd_file] ([od],[os],[text],[tips],[file],[path],[keys],[status],[create_time],[update_time]) VALUES (0,NULL,'网络连接','网络连接','ncpa','ncpa.cpl','ncpa,wangluolianjie',1,'2019-06-22 22:01:14.2261607','2019-06-22 22:01:14.2261607')
INSERT INTO [cmd_file] ([od],[os],[text],[tips],[file],[path],[keys],[status],[create_time],[update_time]) VALUES (0,NULL,'用户账户','用户账户','netplwiz','netplwiz.exe','netplwiz,yonghuzhanghu',1,'2019-06-22 22:01:56.8787592','2019-06-22 22:01:56.8787592')
INSERT INTO [cmd_file] ([od],[os],[text],[tips],[file],[path],[keys],[status],[create_time],[update_time]) VALUES (0,NULL,'Windows功能','启用或关闭Windows功能','OptionalFeatures','OptionalFeatures.exe','OptionalFeatures,windowsgongneng',1,'2019-06-22 22:02:57.8687538','2019-06-22 22:02:57.8687538')
INSERT INTO [cmd_file] ([od],[os],[text],[tips],[file],[path],[keys],[status],[create_time],[update_time]) VALUES (0,NULL,'Windows PowerShell','Windows PowerShell','PowerShell','PowerShell.exe','PowerShell',1,'2019-06-22 22:03:50.5233904','2019-06-22 22:03:50.5233904')
INSERT INTO [cmd_file] ([od],[os],[text],[tips],[file],[path],[keys],[status],[create_time],[update_time]) VALUES (0,NULL,'打印管理','打印管理','printmanagement','printmanagement.msc','printmanagement,dayinguanli',1,'2019-06-22 22:04:56.7728223','2019-06-22 22:04:56.7728223')
INSERT INTO [cmd_file] ([od],[os],[text],[tips],[file],[path],[keys],[status],[create_time],[update_time]) VALUES (0,NULL,'电源选项','选择或自定义电源计划','powercfg','powercfg.cpl','powercfg,dianyuanxuanxiang',1,'2019-06-22 22:05:46.1819802','2019-06-22 22:05:46.1819802')
INSERT INTO [cmd_file] ([od],[os],[text],[tips],[file],[path],[keys],[status],[create_time],[update_time]) VALUES (0,NULL,'步骤记录器','步骤记录器','psr','psr.exe','psr,buzoujiluqi',1,'2019-06-22 22:06:23.5279229','2019-06-22 22:06:23.5279229')
INSERT INTO [cmd_file] ([od],[os],[text],[tips],[file],[path],[keys],[status],[create_time],[update_time]) VALUES (0,NULL,'创建系统修复光盘','创建系统修复光盘','recdisc','recdisc.exe','recdisc,xitongxiufu',1,'2019-06-22 22:07:15.9400532','2019-06-22 22:07:15.9400532')
INSERT INTO [cmd_file] ([od],[os],[text],[tips],[file],[path],[keys],[status],[create_time],[update_time]) VALUES (0,NULL,'资源监视器','资源监视器','resmon','resmon.exe','resmon,ziyuanjianshiqi',1,'2019-06-22 22:07:47.5536730','2019-06-22 22:07:47.5536730')
INSERT INTO [cmd_file] ([od],[os],[text],[tips],[file],[path],[keys],[status],[create_time],[update_time]) VALUES (0,NULL,'系统还原','还原系统文件和设置','rstrui','rstrui.exe','rstrui,xitonghuanyuan',1,'2019-06-22 22:08:30.2422489','2019-06-22 22:08:30.2422489')
INSERT INTO [cmd_file] ([od],[os],[text],[tips],[file],[path],[keys],[status],[create_time],[update_time]) VALUES (0,NULL,'注册表编辑器','注册表编辑器','regedt32','regedt32.exe','regedt32,zhucebiaobianjiqi',1,'2019-06-22 22:09:50.8011462','2019-06-22 22:10:14.3900163')
INSERT INTO [cmd_file] ([od],[os],[text],[tips],[file],[path],[keys],[status],[create_time],[update_time]) VALUES (0,NULL,'组策略结果集','组策略结果集','rsop','rsop.msc','rsop,zucelve,zucelue',1,'2019-06-22 22:11:15.3309013','2019-06-22 22:11:15.3309013')
INSERT INTO [cmd_file] ([od],[os],[text],[tips],[file],[path],[keys],[status],[create_time],[update_time]) VALUES (0,NULL,'备份和还原','备份和还原','sdclt','sdclt.exe','sdclt,beifenhehuanyuan',1,'2019-06-22 22:11:46.1282696','2019-06-22 22:11:46.1282696')
INSERT INTO [cmd_file] ([od],[os],[text],[tips],[file],[path],[keys],[status],[create_time],[update_time]) VALUES (0,NULL,'创建共享文件夹','创建共享文件夹向导','shrpubw','shrpubw.exe','shrpubw,gongxiangwenjianjia',1,'2019-06-22 22:13:18.0358608','2019-06-22 22:13:18.0358608')
INSERT INTO [cmd_file] ([od],[os],[text],[tips],[file],[path],[keys],[status],[create_time],[update_time]) VALUES (0,NULL,'文件签名验证','文件签名验证','sigverif','sigverif.exe','sigverif,wenjianqianmingyanzheng',1,'2019-06-22 22:13:51.9507886','2019-06-22 22:13:51.9507886')
INSERT INTO [cmd_file] ([od],[os],[text],[tips],[file],[path],[keys],[status],[create_time],[update_time]) VALUES (0,NULL,'Windows激活','查看系统激活信息','slui','slui.exe','slui,jihuo',1,'2019-06-22 22:15:02.2656219','2019-06-22 22:15:02.2656219')
INSERT INTO [cmd_file] ([od],[os],[text],[tips],[file],[path],[keys],[status],[create_time],[update_time]) VALUES (0,NULL,'截图工具','截图工具','snippingtool','snippingtool.exe','snippingtool,jietugongju',1,'2019-06-22 22:15:34.7514342','2019-06-22 22:15:34.7514342')
INSERT INTO [cmd_file] ([od],[os],[text],[tips],[file],[path],[keys],[status],[create_time],[update_time]) VALUES (0,NULL,'录音机','录音机','soundrecorder','soundrecorder.exe','soundrecorder,luyinji',1,'2019-06-22 22:16:02.8039032','2019-06-22 22:16:02.8039032')
INSERT INTO [cmd_file] ([od],[os],[text],[tips],[file],[path],[keys],[status],[create_time],[update_time]) VALUES (0,NULL,'系统属性','系统属性','sysdm','sysdm.cpl','sysdm,xitongshuxing',1,'2019-06-22 22:17:17.1167976','2019-06-22 22:17:17.1167976')
INSERT INTO [cmd_file] ([od],[os],[text],[tips],[file],[path],[keys],[status],[create_time],[update_time]) VALUES (0,NULL,'系统配置编辑器','系统配置编辑器','sysedit','sysedit.exe','sysedit,xitongpeizhibianjiqi',1,'2019-06-22 22:17:54.7607737','2019-06-22 22:17:54.7607737')
INSERT INTO [cmd_file] ([od],[os],[text],[tips],[file],[path],[keys],[status],[create_time],[update_time]) VALUES (0,NULL,'任务计划程序','任务计划程序','taskschd','taskschd.msc','taskschd,renwujihuachengxu',1,'2019-06-22 22:18:53.1844659','2019-06-22 22:18:53.1844659')
INSERT INTO [cmd_file] ([od],[os],[text],[tips],[file],[path],[keys],[status],[create_time],[update_time]) VALUES (0,NULL,'日期和时间','日期和时间','timedate','timedate.cpl','timedate,riqiheshijian',1,'2019-06-22 22:19:18.7674530','2019-06-22 22:19:18.7674530')
INSERT INTO [cmd_file] ([od],[os],[text],[tips],[file],[path],[keys],[status],[create_time],[update_time]) VALUES (0,NULL,'用户账户控制设置','用户账户控制设置','UserAccountControlSettings','UserAccountControlSettings.exe','UserAccountControlSettings,yonghuzhanghukongzhi',1,'2019-06-22 22:20:04.5029271','2019-06-22 22:20:04.5029271')
INSERT INTO [cmd_file] ([od],[os],[text],[tips],[file],[path],[keys],[status],[create_time],[update_time]) VALUES (0,NULL,'辅助工具管理器','辅助工具管理器','utilman','utilman.exe','utilman,fuzhugongju',1,'2019-06-22 22:20:48.6199461','2019-06-22 22:20:48.6199461')
INSERT INTO [cmd_file] ([od],[os],[text],[tips],[file],[path],[keys],[status],[create_time],[update_time]) VALUES (0,NULL,'高级安全Windows Defender 防火墙','高级安全Windows Defender 防火墙','wf','wf.msc','wf,fanghuoqiang',1,'2019-06-22 22:21:36.5308161','2019-06-22 22:21:36.5308161')
INSERT INTO [cmd_file] ([od],[os],[text],[tips],[file],[path],[keys],[status],[create_time],[update_time]) VALUES (0,NULL,'Windows 传真和扫描','Windows 传真和扫描','wfs','wfs.exe','wfs,chuanzhenhesaomiao',1,'2019-06-22 22:22:19.4851734','2019-06-22 22:22:19.4851734')
INSERT INTO [cmd_file] ([od],[os],[text],[tips],[file],[path],[keys],[status],[create_time],[update_time]) VALUES (0,NULL,'扫描仪和照相机向导','扫描仪和照相机向导','wiaacmgr','wiaacmgr.exe','wiaacmgr,saomiaoyihezhaoxiangjixiangdao',1,'2019-06-22 22:23:02.4246039','2019-06-22 22:23:02.4246039')
INSERT INTO [cmd_file] ([od],[os],[text],[tips],[file],[path],[keys],[status],[create_time],[update_time]) VALUES (0,NULL,'打开windows管理体系结构(WMI)','打开windows管理体系结构(WMI)','wmimgmt','wmimgmt.msc','wmimgmt',1,'2019-06-22 22:23:28.8313050','2019-06-22 22:23:28.8313050')
INSERT INTO [cmd_file] ([od],[os],[text],[tips],[file],[path],[keys],[status],[create_time],[update_time]) VALUES (0,NULL,'操作中心','操作中心','wscui','wscui.cpl','wscui',1,'2019-06-22 22:23:53.0318672','2019-06-22 22:23:53.0318672')