# DTscope_dome

设计：	1812用于问询robot广播
	1813用于接收robot广播
	1814用于专有tx-robot
	1815用于专有rx-robot

robot:	1813用于握手广播
	1815用于专有通讯

问询协议：<广播>
Send:#RM-DT=DCY_ROBOT:#END
Rev:#RM-DT=REP_DCY:IM=<type>;STA=<state_code>;[TYPE=<is_rm?>;]#END	//state=0:Off_line  1:On_line_free  2:On_line_busy  3:On_line_connectOK
握手协议：<点播>
Send:#RM-DT=CNET:TAR=<type>;TIP=<tar_ip>;CIP=<ip>;CPT=<port>;#END
rev:#RM-DT=RCNET:TIP=<tip>;CIP=<ip>;CPT=<port>;#END
wait 1s<等待切换到透传>
rev:#RM-DT=RCNET:OK;#END
Send:#RM-DT=CNET:OK;#END

已连接的HOST被问询:<广播>
Rev:#RM-DT=DCY_ROBOT:#END
Send:#RM-DT=REP_DCY:IM=<type>;STA=<state>;#END	//state=已连接

数据传输协议：<点播>	//需要有：时间间隔信息，该信息一般不变，可放在握手协议，还需要有：每包几个字节数据；算了，该信息放在CMD中
配置信息包：
Send:#RM-DT=DATA_INFO:TIM=<tim_interavl>;ABYTES=<byte_numbers>;TYPE=<s2.s2.s2.s2>#END	//TYPE的值含义是：1数据类型+1字节数.2数据类型+2字节数……
Rev:#RM-DT=INFOOK:#END
数据包：
Rev:#RM-DT=DATA:<XX****160****XX>;#END

心跳包：
Send:#RM-DT=HTBEAT:#END

正常退出：
Rev:Send:#RM-DT=CMD:EXIT;#END


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