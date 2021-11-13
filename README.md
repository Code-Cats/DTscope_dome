# DTScope远程调试器

为了在对机器人进行运动控制调试时更便捷的实时分析数据而设计的远程数据查看/调试软件/协议

## 功能说明

局域网内机器人自动发现协议

可选指定机器人连接

远程查看数据，显示波形

机器人端可动态配置回传数据通道数量、格式、帧率

根据包序统计丢包率

TODO:

更稳定的数据处理（下位机使用esp8266通讯时偶发断流BUG）

更完善的数据导出功能

通讯协议数据加密

波形拖动功能（需要对控件进行更改）

远程可动态配置回传数据（选择变量）

## 使用截图

![image-20201208114432688](https://gitee.com/code-cat/ImageHosting/raw/master/images/20201208114435.png)

![image-20201208114503428](https://gitee.com/code-cat/ImageHosting/raw/master/images/20201208114506.png)



## DTscope 专有通讯协议开发

目标：局域网自动发现，远程连接，实时数据回传，动态配置。以下冒号后为ASCII字符

#### 上位机端口设计：

​	1812用于问询robot广播  
​	1813用于接收robot广播  
​	1814用于专有tx-robot  
​	1815用于专有rx-robot  

#### robot端口:	

​	1813用于握手广播  
​	1815用于专有通讯  

#### 协议设计：

##### 问询协议：<广播>

Send:#RM-DT=DCY_ROBOT:#END  
Rev:#RM-DT=REP_DCY:IM=<type>;STA=<state_code>;[TYPE=<is_rm?>;]#END	//state=0:Off_line  1:On_line_free  2:On_line_busy  3:On_line_connectOK

##### 握手协议：<点播>

Send:#RM-DT=CNET:TAR=<type>;TIP=<tar_ip>;CIP=<ip>;CPT=<port>;#END  
rev:#RM-DT=RCNET:TIP=<tip>;CIP=<ip>;CPT=<port>;#END  
wait 1s<等待切换到透传>  
rev:#RM-DT=RCNET:OK;#END  
Send:#RM-DT=CNET:OK;#END  

已连接的HOST被问询:<广播>  
Rev:#RM-DT=DCY_ROBOT:#END  
Send:#RM-DT=REP_DCY:IM=<type>;STA=<state>;#END	//state=已连接  

##### 数据传输协议：<点播>	//需要有：时间间隔信息，该信息一般不变，可放在握手协议，还需要有：每包几个字节数据；算了，该信息放在CMD中

配置信息包：  
Send:#RM-DT=DATA_INFO:TIM=<tim_interavl>;ABYTES=<byte_numbers>;TYPE=<s2.s2.s2.s2>;#END	//TYPE的值含义是：1数据类型+1字节数.2数据类型+2字节数……	目前只支持：  s8,u8,s16,u16,s32,u32,float  
Rev:#RM-DT=INFOOK:#END  
数据包：  
Rev:#RM-DT=DATA:Sn=<XX****160****XX>;#END  

心跳包：  
Send:#RM-DT=HTBEAT:#END  

正常退出：  
Rev:Send:#RM-DT=CMD:EXIT;#END  

##### //完整的流程示例：

#RM-DT=REP_DCY:IM=SENTRY1;STA=1;#END  
#RM-DT=RCNET:TIP=192.168.1.74;CIP=192.168.1.25;CPT=1815;#END  
#RM-DT=RCNET:OK;#END  
#RM-DT=DATA_INFO:TIM=1;ABYTES=10;TYPE=<s2.s2.s2.s2.s2>;#END  
Rev:#RM-DT=DATA:Sn=<XX****160****XX>;#END  
//单ch实例  
#RM-DT=REP_DCY:IM=SENTRY1;STA=1;#END  
#RM-DT=RCNET:TIP=192.168.1.74;CIP=192.168.1.25;CPT=1815;#END  
#RM-DT=RCNET:OK;#END  
#RM-DT=DATA_INFO:TIM=1;ABYTES=2;TYPE=s2;  
#END  

#RM-DT=DATA:S0=2323;#END

##### 程序内机器人定义

{"SENTRY1",ROBOT_Type.Sentry1 },  
{"SENTRY2",ROBOT_Type.Sentry2 },  
{"INFANTRY1",ROBOT_Type.Infantry1 },  
{"INFANTRY2",ROBOT_Type.Infantry2 },  
{"HERO1",ROBOT_Type.Hero1 },  
{"HERO2",ROBOT_Type.Hero2 },  
{"ENGINEER1",ROBOT_Type.Engineer1 },  
{"ENGINEER2",ROBOT_Type.Engineer2 },  
{"UAV",ROBOT_Type.Uav },  
{"OTHER1",ROBOT_Type.Other1 },  
{"OTHER2",ROBOT_Type.Other2 },  
{"OTHER3",ROBOT_Type.Other3 },  
