# DTscope_dome

设计：	1812用于问询robot广播
	1813用于接收robot广播
	1814用于专有tx-robot
	1815用于专有rx-robot

robot:	1813用于握手广播
	1814用于专有通讯

问询协议：<广播>
Send:#RM-DT=DCY_ROBOT#END
Rev:#RM-DT=REP:IM=<type>;STA=<state>#END
握手协议：<点播>
Send:#RM-DT=CNET:TAR=<type>;TIP=<tar_ip+1>;CIP=<ip>;CPT=<port>#END
rev:#RM-DT=RCNET:TIP=<tip+1>;CIP=<ip>;CPT=<port>#END
wait 1s<等待切换到透传>
rev:#RM-DT=RCNET:OK#END
Send:#RM-DT=CNET:OK#END

已连接的HOST被问询:
Rev:#RM-DT=DCY_ROBOT#END
Send:#RM-DT=REP:IM=<type>;STA=<state>#END	//state=已连接