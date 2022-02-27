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
		- ARRaycastHit 구조체 내 position과 rotation 정보를 저장한 후 Idicator의 위치와 회전 값을 조정 
	3. 	
	4. 
