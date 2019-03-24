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