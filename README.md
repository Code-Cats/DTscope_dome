# DTscope_dome

��ƣ�	1812������ѯrobot�㲥
	1813���ڽ���robot�㲥
	1814����ר��tx-robot
	1815����ר��rx-robot

robot:	1813�������ֹ㲥
	1814����ר��ͨѶ

��ѯЭ�飺<�㲥>
Send:#RM-DT=DCY_ROBOT#END
Rev:#RM-DT=REP:IM=<type>;STA=<state>#END
����Э�飺<�㲥>
Send:#RM-DT=CNET:TAR=<type>;TIP=<tar_ip+1>;CIP=<ip>;CPT=<port>#END
rev:#RM-DT=RCNET:TIP=<tip+1>;CIP=<ip>;CPT=<port>#END
wait 1s<�ȴ��л���͸��>
rev:#RM-DT=RCNET:OK#END
Send:#RM-DT=CNET:OK#END

�����ӵ�HOST����ѯ:
Rev:#RM-DT=DCY_ROBOT#END
Send:#RM-DT=REP:IM=<type>;STA=<state>#END	//state=������