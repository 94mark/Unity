지형 인식을 이용한 자동차 AR 콘텐츠 제작
---
### 프로젝트 개요
	1. 개발 인원/기간 및 포지션
		- 1인, 총 3일 소요
		- 프로그래밍 (AR환경 구축 및 개발)
	2. 개발 환경
		- Unity 2021.3.13f
		- AR build : Galaxy S20
		- 언어 : C#
		- OS : Window 10

### 개발 순서
	1. 바닥 지형 인식
	2. 자동차 모델링 생성 및 조작
	3. 사용자 편의 기능 추가

### 핵심 구현 내용
	1. 바닥 지형 인식
		- AR Session Origin -> AR Plane Manager의 Detection Mode를 "Horizontal"로 변경해 바닥 감지
		- 감지된 바닥을 시각적으로 실시간 표현하기 위해 AR Plane Mesh Visualizer 컴포넌트가 추가된 DetectedPlane 오브젝트 생성
	2. AR Raycast를 이용해 바닥면에 Indicator 표식 출력
		- ARRaycastManager.Raycast() 함수 선언, Screen 중앙 지점에서 Ray를 발사했을 때 Plane 형태의 Trackable Type이 있다면 Indicator 활성화
		- ARRaycastHit 구조체 내 position과 rotation 정보를 저장한 후 Indicator의 위치와 회전 값을 조정 
	3. 모델링 생성 및 조작
		- 터치 입력 시 TouchPhase.Began 함수를 사용하여 오브젝트 생성
		- 게임 오브젝트를 회전시키기 위해 Rotate(회전축, 회전 각도) 함수 사용, 회전 축은 자동차 기울기를 고려해 로컬 좌표(transform.up) 전달, 회전 각도로는 deltaPosition.x 값에 -1을 곱해 터치 이동방향과 회전방향 일치
	4. 앱 실행 중 화면꺼짐 방지 기능 구현
		- AR 카메라가 사물을 인식하는 과정에서 dml 시간 소요로 인해 입력이 없어 슬립 상태로 전환되는 경우 발생
		- 디스플레이 절전 관련 Screen 클래스의 sleepTimeout 속성을 NeverSleep으로 선언하여 절전 모드 해제

### 문제 해결 내용
	1. 자동차 프리팹의 머터리얼 색상 변경 문제
		- 테스트 시 색상 변경 입력값이 거리에 따라 다르게 나오는 문제 발생  
		- SportCar 오브젝트의 Mesh Renderer 컴포넌트에 적용된 Paint 머터리얼이 LOD Group 컴포넌트에 영향을 받고 있음을 발견
		- 카메라와 자동차 간의 거리에 따라 Lod1 ~ LOD3 (3단계)의 메시와 머터리얼이 사용 중
		-  SportCar20_Paint_LOD01~SportCar20_Paint_LOD03까지 LOD별 오브젝트 3개에 대한 GameObject/Material/Color32를 배열 변수로 선언하여 문제 해결
 
