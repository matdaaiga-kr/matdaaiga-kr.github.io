Title: 맞다톤 2026 후기
Lead: 맞다AI가의 첫 해커톤 후기
Published: 2026-08-22
Tags:
  - AI
  - Hackathon
  - Vibe Coding
  - Cloud
  - Azure
  - GitHub
  - Copilot
  - MVP
  - Developer
HeroImage: /images/posts/2026/08/matdathon-2026-hero.png
---

2026년 8월 22일, 맞다AI가의 첫 해커톤 **맞다톤 2026**이 개최되었습니다.

![session0][image-00]


맞다톤은 전공과 경력에 상관없이 누구나 참여할 수 있는 해커톤으로, 참가자들은 아침에 아이디어를 공유하고 저녁에는 배포된 MVP를 완성할 수 있었습니다.

맞다톤 진행에 앞서 유저스틴님과 이동수님의 세션이 있었습니다. 

---

### Session1. 바이브 코딩으로 AI 에이전트 만들어 보기 | 유저스틴

![session1][image-01]

Microsoft에서 Principal Developer Advocate으로 재직 중인 유저스틴님께서는 에이전틱 AI 앱을 설계하는 방법과 멀티 에이전트 시스템, Aspire 오케스트레이션 등의 프로젝트 설계법을 소개하셨습니다. 

이어서 GitHub Copilot으로 앱을 개발할 때 AGENTS.md, PRD.md, TRD.md 등의 파일을 활용하는 방법을 알려주셔서 참가자들이 해커톤에서 바로 적용해볼 수 있었습니다. 

- [유저스틴님의 발표자료 확인하기][justin-file]

- [유저스틴님이 알려주는 다양한 개발 소식 링크드인에서 받아보기][justin-sns]

### Session2. 서비스도 딸깍, 배포도 딸깍! Azure Skills로 쉽게 배포하기 | 이동수

![session2][image-02]

[Microsoft 학생 홍보대사][msa]로 활동 중인 이동수님은 GitHub Copilot App으로 아이디어 구상부터 계획, 개발, 테스트, Azure 배포까지 이어지는 해커톤 개발 흐름을 소개했습니다. 특히 AI에게 무엇을 어떻게 시킬지에 초점을 맞춰 참가자들이 GitHub Copilot을 잘 활용할 수 있는 방법을 알려주셨습니다.

- [이동수님의 발표자료 확인하기][dongsulee-file]

- [이동수님이 알려주는 다양한 개발 소식 링크드인에서 받아보기][dongsulee-sns]

---

이번 맞다톤의 주제는 "나의 업무 효율을 높힐 수 있는 개인 생산성 향상 앱"이었습니다.

유저스틴님과 이동수님의 세션 이후 참가자들은 GitHub Copilot을 활용한 바이브 코딩으로 앱을 개발하고 Azure로 배포까지 진행했습니다. 

---

### 1위: [한끼픽(HankkiPick)](https://github.com/matdaaiga-kr/hankkipick) - 크크 팀

![session3][image-03]

자취생의 “오늘 뭐 먹지?”라는 고민에서 출발해, AI가 사용자의 답변에 따라 질문을 실시간으로 조정하고 메뉴 추천 → 1인분 장보기 비용 계산 → 레시피 → 남은 재료 활용까지 한 번에 연결한 서비스입니다. 

GitHub Copilot SDK와 Microsoft Agent Framework를 활용한 다중 AI 에이전트 구조와 장애 상황을 고려한 폴백 로직 등 기술적 완성도가 돋보였으며, 단순한 메뉴 추천을 넘어 자취생의 실제 식사 여정 전체를 하나의 사용자 경험으로 설계했다는 점에서 높은 기획적 완성도를 보여주었습니다. 

### 2위: [Chore Forge](https://github.com/matdaaiga-kr/chore-forge) - NDD 팀

![session4][image-04]

기존 업무 자동화 도구들이 서비스별로 분산되어 있어 여러 창과 플랫폼을 오가며 자동화를 만들고 관리해야 하는 불편함에서 출발한 서비스입니다. 

반복 업무를 자연어로 설명하면 AI가 실행 가능한 자동화 도구를 생성·검증하고, 이를 나만의 자동화 스킬로 한곳에 저장해 반복적으로 재사용할 수 있도록 설계했습니다. 

Spec·Builder·Verifier로 역할을 나눈 다중 에이전트 구조와 샌드박스 실행, 자동 수정·검증 과정을 통해 안정성을 높였으며, 분산된 자동화 경험을 개인화된 업무 자산으로 통합한다는 기획적 차별성이 돋보였습니다. 

### 3위: [PAGO](https://github.com/matdaaiga-kr/pago) - Aforia 팀

![session5][image-05]

낯선 기술 분야에서 “무엇을, 어떤 순서로 공부해야 할지”를 찾는 복잡한 리서치 과정을 AI로 구조화한 학습 탐색 서비스입니다. 

시장·학계·커뮤니티 데이터를 각각 분석하는 AI 에이전트를 병렬로 구성하고, 결과를 통합해 유망 세부 주제를 시각화한 뒤 단계별 탐색과 2주 학습 로드맵까지 제공합니다. 

Microsoft Agent Framework 기반 5종 에이전트 오케스트레이션, OpenAlex·Hacker News 등 실제 공개 데이터 연동과 Azure 배포까지 1인 개발로 구현했다는 기술적 완성도와, 정보 탐색을 실제 학습 계획으로 연결한 기획력이 돋보였습니다. 

---

수상 여부에 관계없이 참가해주신 여러분들 모두 고생많으셨습니다. 

이번 행사를 제대로 운영할 수 있도록 지원해주신 Microsoft, GitHub, OpenUP 관계자 분들께 다시 한 번 감사의 말씀을 전합니다.

앞으로도 맞다AI가에서는 대구/경북 지역의 개발자들이 빠르게 최신의 AI 기술과 클라우드 네이티브 기술을 접할 수 있도록 다양한 이벤트를 준비할 예정입니다. 다가올 9월, 10월 행사도 많은 기대 부탁합니다!

[맞다AI가 링크드인으로 행사 소식 바로 받아보기][matdaaiga-sns]

[image-00]: /images/posts/2026/08/matdathon-2026-retro-00.jpg
[image-01]: /images/posts/2026/08/matdathon-2026-retro-01.jpg
[image-02]: /images/posts/2026/08/matdathon-2026-retro-02.jpg
[image-03]: /images/posts/2026/08/matdathon-2026-retro-03.jpg
[image-04]: /images/posts/2026/08/matdathon-2026-retro-04.jpg
[image-05]: /images/posts/2026/08/matdathon-2026-retro-05.jpg

[justin-file]: /archived/2026/08/justin.pdf
[dongsulee-file]: /archived/2026/08/dongsulee.pdf

[justin-sns]: https://linkedin.com/in/justinyoo
[msa]: https://mvp.microsoft.com/studentambassadors
[dongsulee-sns]: https://www.linkedin.com/in/dongsu-leee/
[matdaaiga-sns]: https://www.linkedin.com/company/matdaaiga/
