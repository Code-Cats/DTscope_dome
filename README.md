# DTscope_dome

��ƣ�	1812������ѯrobot�㲥
	1813���ڽ���robot�㲥
	1814����ר��tx-robot
	1815����ר��rx-robot

robot:	1813�������ֹ㲥
	1815����ר��ͨѶ

��ѯЭ�飺<�㲥>
Send:#RM-DT=DCY_ROBOT:#END
Rev:#RM-DT=REP_DCY:IM=<type>;STA=<state_code>;[TYPE=<is_rm?>;]#END	//state=0:Off_line  1:On_line_free  2:On_line_busy  3:On_line_connectOK
����Э�飺<�㲥>
Send:#RM-DT=CNET:TAR=<type>;TIP=<tar_ip>;CIP=<ip>;CPT=<port>;#END
rev:#RM-DT=RCNET:TIP=<tip>;CIP=<ip>;CPT=<port>;#END
wait 1s<�ȴ��л���͸��>
rev:#RM-DT=RCNET:OK;#END
Send:#RM-DT=CNET:OK;#END

�����ӵ�HOST����ѯ:<�㲥>
Rev:#RM-DT=DCY_ROBOT:#END
Send:#RM-DT=REP_DCY:IM=<type>;STA=<state>;#END	//state=������

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