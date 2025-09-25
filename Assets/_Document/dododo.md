인수 인계 같은 느낌

SkillContainer => 현재 스킬 관리 클래스

skillInstance => skillData를 기반으로 생성한 객체

skillStat => 스킬에서 사용하는 스킬 전용 스텟
skillModifier => 스킬 스탯 수정자

skillAction => 스킬 이벤트에 따라 실행되는 스킬 본체
	ㄴ skillStatModifier로 스킬 스탯을 수정
	ㄴ SOSkillActionModule 스킬의 행동을 결정하는 클래스 
	예) 프로젝타일 생성 
		ㄴ> SkillInstance를 객체로 넘겨서 이벤트를 발생시 이벤트 실행 
			ㄴ> DamageModule에서 데미지 생성 및 전달
	예) 근접공격
		ㄴ> SOSkillActionModule 의 DamageModule를 바로 실행 
			ㄴ이경우에는 투사체 같은 객체를 따로 생성하지 않기 때문에 데미지 전달만 해도 됨



