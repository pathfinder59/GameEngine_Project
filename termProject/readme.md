# 게임 엔진 기말 프로젝트 게임공학과 2015180005 김영완

## 게임개요
- 제목: Momodora’s adventure
- 장르: 2D Adventure 
- 플랫폼: PC

## 게임소개
### 소개
- 미로를 탐험하면서 집을 찾아 내야하는 2D Adventure

### 게임 설명
- 게임 맵 내의 적들 및 함정을 통과하면서 미로를 클리어해야 한다.  
- 시간 제한은 없고 Hp는 3으로 시작한다. (총 세번의 기회가 있다고 보면 됨)  
- 실패 조건: Hp가 0이 될 경우 GameOver(ex: 몬스터와의 충돌, 맵밖으로 아웃, 함정과의 충돌)  
- 클리어 조건:맵 내에 숨겨져 있는 집에 도착시  클리어  
- 가는 길의 도중마다 설명이 적혀있는 표지판이 존재(힌트)  
- 신발 모양의 아이템은 캐릭터의 점프 높이를 올려준다 

### 조작 설명
- 이동: 방향키
- 공격(활 공격): Ctrl
- 점프: Space

### 적용된 기술
1. 시야의 적이 있을 경우 쫒아가는 몬스터의 알고리즘
- 플레이어와 적의 벡터를 뺀값의 크기가 시야 크기보다 작을 경우 몬스터는 idle에서 walk애니메이션으로 전환되고
플레이어를 향한 방향벡터로 이동 크기에 deltatime만큼 곱한 값이 translate하도록 하였음.

2. 몬스터 처치 시 파티클 발생
- 몬스터가 동시에 죽거나 한 파티클 종료전에 다른 몬스터가 죽을 일이 없기 때문에 하나의 파티클을 미리 만들어두고
각 몬스터가 죽을 시 미리 만들어둔 파티클 시스템을 활성화 시키고 사망한 오브젝트의 자리에 위치하게 해줌으로써 구현함.(object pooling)
파티클 시스템은 pSystem이라는 스크립트를 갖고 활성화 후 5초가 지나면 다시 비활성화가 되도록 작성하였음.
결과적으로 모든 몬스터들이 하나의 파티클 시스템으로 공유해서 사용한다.

3. Joint 활용 
-Distance Joint를 이용해서 로프 오브젝트와 폭탄을 연결하여 마치 폭탄이 천장의 줄에 매달린 것처럼 보이도록 하였음.

4. Object Pooling
- 특정 갯수만 사용하게 되는 화살 오브젝트는 ObjectPooler를 통해 비활성화 오브젝트 20개를 만들어놓고 시작하여 필요할 때마다
비활성화 물체를 찾아서 활성화시켜서 사용하도록 한다. 만일 20개를 넘게 사용할 경우 그 때마다 새로 하나씩 생성한다.

5. eventHandler
- GameManager에서 실행시 EventHandler에 "PlayerDied" 이벤트 등록, 호출 함수는 플레이어를 시작위치로 되돌리는 함수이다.
그 후 플레이어의 스크립트에서 몬스터,함정과 충돌하거나 맵밖으로 떨어져 나갈 시 "PlayerDied" 이벤트를 호출하여 시작위치로 되돌아간다.

6. IEnumerator
- 정해진 시간만큼 사라졌다가 다시 생기는 행동을 반복하는 MagicBlock에서 사용된다.
사라졌다 나타나는 행동들의 코드를 전부 IEnumerator에 담아두고 특정 행동을 마칠때마다 yield return null호출.
그리고 다음 Update에서 이를 또 호출하여 다음 동작을 수행하도록 하게 되어 있다.
- 순서: 등장 지속시간 측정 -> 서서히 사라지는 애니메이션 -> 사라져있을 동안의 시간 측정 -> 서서히 등장하는 애니메이션

## 에셋 출처(스프라이트)
카호: https://www.spriters-resource.com/pc_computer/momodorareverieunderthemoonlight/sheet/86754/  

몬스터,표지판,타일맵,집: 유니티 에셋스토어의 SunnyLand,Monster 이용  

석궁: https://www.flaticon.com/free-icon/crossbow_942514  

화살: http://thatgamesguy.co.uk/cpp-game-dev-32/  

신발 아이콘: 구글링  

불기둥 : https://www.shutterstock.com/ko/image-illustration/cartoon-fire-flame-sheet-sprite-animation-1101712109  

점프용 버섯: https://opengameart.org/content/mushroom-game-character-for-background-object-or-enemy  

폭탄: https://steemit.com/pixelart/@loomy/pixel-art-items-i-am-using-in-my-current-project  
