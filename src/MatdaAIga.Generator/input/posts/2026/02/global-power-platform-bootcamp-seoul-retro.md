Title: Global Power Platform Bootcamp Seoul 2026 후기
Lead: 프록토 참여 후기
Published: 2026-02-22
Tags:
  - Power Platform
  - Bootcamp
  - AI
  - Agent
  - Azure
  - MCP
  - Microsoft Agent Framework
  - Copilot Studio
  - GPPB2026
HeroImage: /images/posts/2026/02/global-power-platform-bootcamp-seoul/global-power-platform-bootcamp-seoul-retro.opening.JPG
---

2026년 2월 21일(토), Microsoft가 후원하는 글로벌 커뮤니티 행사 [Global Power Platform Bootcamp Seoul 2026][gppb-event]이 서울 광화문 마이크로소프트 오피스에서 열렸습니다. Global Power Platform Bootcamp Seoul 2026 행사는 Copilot Studio를 활용하여 AI를 처음부터 끝까지 손으로 만들어보는 실습 중심 행사로, 주말 아침임에도 불구하고 열정 넘치는 참가자들로 가득 찬 하루였습니다.

![행사 전경][image-01]

이번 행사는 참여자 수준에 따라 A(초급), B(중급), C(고급) 세 가지 트랙으로 구성되었습니다. 그 중 저희가 프록토로 함께한 C 트랙의 제목은 "Agent Framework + Agent : 코딩 지옥"이었는데요, 이름만 봐도 예사롭지 않죠🔥.

C 트랙은 [Microsoft Agent Framework(MAF)][maf]를 활용해 AI 에이전트를 코드로 직접 구현하고 Azure에 배포해본 뒤, 그 힘들게 만들었던 기능들을 [Copilot Studio][copilot-studio]로 얼마나 쉽게 재현할 수 있는지 비교해보는 구성입니다. 개발자 입장에서는 "아, 이게 이렇게 추상화되는구나"를 체감하고, 비개발자 입장에서는 "이 뒤에 이런 복잡한 게 있었구나"를 배울 수 있는, 양쪽 모두에게 울림이 있는 커리큘럼이었습니다.

콘텐츠는 C 트랙 발표자이신 [유저스틴][yoojustin-sns]님이 제작하시고, 프록토들이 흐름 검토 및 오류 수정에 함께 참여하는 방식으로 완성된 자료입니다. 워크샵 자료는 [GitHub][gh-sample]에 공개해두었기에 당일 따라가지 못한 부분도 이후에 자기주도학습으로 완주할 수 있습니다.

![발표자 : 유저스틴][image-03]


### 🧑‍💻 프록토로서의 하루

프록토 역할을 한 마디로 정의하자면, "참가자가 막히기 전에 먼저 막혀보는 사람"입니다. 행사 전 워크샵 자료를 직접 따라해보며 오류가 생길 지점을 미리 찾아내고, 참가자들이 어느 부분에서 질문할지 예측해두어야 합니다.

C 트랙에서는 발표자가 각 세션에 대해 10분 내외로 설명한 뒤 나머지 시간동안 참가자들이 [GitHub][gh-sample] 자료와 함께 실습하는 방식으로 진행했는데, 이때 실습 시간 동안 프록토는 곳곳에 서서 실습 도중 막히는 참가자들의 문제를 빠르게 해소하며 다음 실습 내용으로 넘어갈 수 있도록 돕습니다.

이번 C 트랙에는 총 3인의 프록토([이시영][siyoung-sns]님, [김근희][geunhee-sns]님, [황지현][jihyeon-sns]님)가 함께했습니다. 전날 새벽까지 자료를 검토하느라 3시간 남짓 잠을 잤지만, 참가자분들이 막히는 것 없이 술술 따라오는 모습을 보는 것만으로도 충분히 보람 있는 하루였습니다.

![프록토 현장 : 근희][image-02]


### 📚 7교시 워크샵 여정

총 7교시로 구성된 긴 하루였지만, C 트랙 참가자들의 완주율은 인상적이었습니다. 전문 개발자를 대상으로 한 트랙답게 질문 빈도도 예상보다 낮았고, 계획한 시간보다 일찍 마무리될 정도였습니다. 개인적으로 더 놀라왔던 점은, 비개발자 참가자가 몇 분 계셨음에도 불구하고 모두 완주하셨다는 점입니다. AI 에이전트 개발에 대한 높은 관심과 열정을 느낄 수 있었던 부분이었습니다(저희 프록토들이 최대한 문제 지점을 줄이려고 꼼꼼히 검토한 덕분이기도 하겠죠☺️?). 


#### 1교시: 개발 환경 설정 (10:40 - 11:40)

워크샵의 시작은 개발 환경 설정부터입니다. Azure 구독, GitHub Codespaces 인스턴스 생성 등 필요한 도구들을 준비하고 프로젝트 초기 설정을 완료했습니다.

📝 [실습 자료 보기][session-01]


#### 2교시: 단일 에이전트 개발하기 (11:40 - 12:30)

Microsoft Agent Framework를 사용해 첫 번째 AI 에이전트를 개발했습니다. Agent 클래스를 구현하고 프롬프트를 설정하며, Tool 형태의 플러그인을 연동하여 에이전트에 기능을 추가하는 방법을 실습했습니다.

📝 [실습 자료 보기][session-02]


#### 🥪 점심시간 (12:30 - 13:30)

점심 식사(크라잉치즈버거)와 커피(빽다방)까지 세심하게 챙겨주신 운영팀 덕분에, 스태프와 참가자 모두 에너지를 충전할 수 있었습니다.


#### 3교시: 다중 에이전트 개발하기 (13:30 - 14:30)

여러 에이전트가 협업하는 다중 에이전트 시스템을 구현했습니다. 에이전트 간 대화를 조율하고, 복잡한 작업을 여러 전문 에이전트가 협력하여 처리하는 패턴을 배웠습니다.

📝 [실습 자료 보기][session-03]


#### 4교시: Aspire로 오케스트레이션 하기 (14:30 - 15:30)

.NET Aspire를 활용해 에이전트 애플리케이션을 오케스트레이션하고, 분산 시스템을 효과적으로 관리하는 방법을 실습했습니다.

📝 [실습 자료 보기][session-04]


#### 5교시: MCP 서버 연동하기 (15:30 - 16:30)

MCP(Model Context Protocol) 서버를 직접 개발하고 Microsoft Agent Framework와 연동하는 세션입니다. MCP는 AI 에이전트가 외부 도구와 서비스를 표준화된 방식으로 연결하는 프로토콜로, AI 에이전트 생태계의 핵심 기술입니다.

에이전트가 외부 API, 데이터베이스, 도구들과 어떻게 상호작용하는지 코드 레벨에서 직접 구현해볼 수 있었습니다.

📝 [MCP 서버 개발][session-05] | [MAF와 MCP 연동][session-06]


#### 6교시: Copilot Studio 연동하기 (16:30 - 17:30)

Copilot Studio에서 Low-code 방식으로 에이전트를 개발하고, 앞서 만든 MCP 서버를 연동했습니다. 코드로 힘들게 구현했던 기능들이 클릭 몇 번으로 재현되는 것을 보며 Low-code의 강력함을 체감했습니다.

📝 [실습 자료 보기][session-07]


#### 7교시: 각 트랙 마무리 (17:30 - 18:00)

마지막으로 행사 종료 전 설문조사를 진행하고, 참가자들과 질의응답 시간을 가졌습니다. 

---

맞다AI가로서도, 개인으로서도 많은 자극을 얻은 하루였습니다. AI 에이전트 기술이 코드 레벨부터 Low-code까지 스펙트럼이 넓어지고 있는 지금, 그 전 과정을 하루 안에 체험할 수 있는 행사는 흔하지 않습니다. 다음 번에도 이런 기회가 생긴다면 기꺼이 다시 프록토로 함께하겠습니다. 이번 행사를 제대로 운영할 수 있도록 지원해주신 [Microsoft][ms], [Office Tutor][officetutor], 그리고 Global Power Platform Bootcamp 운영팀 관계자 분들께 다시 한 번 감사의 말씀을 전합니다.

행사에서 사용했던 실습 자료는 아래 링크를 참조하세요.

📝 [Power Platform Bootcamp Workshop 자료][gh-sample]

앞으로도 맞다AI가는 이처럼 현장에서 기술을 직접 경험하는 자리에 꾸준히 함께하겠습니다. 3월 맞다AI가 밋업도 곧 안내할 예정이니, 많이 기대해 주세요!

[맞다AI가 링크드인으로 행사 소식 바로 받아보기][matdaaiga-sns]

[image-01]: /images/posts/2026/02/global-power-platform-bootcamp-seoul/global-power-platform-bootcamp-seoul-retro.opening.JPG
[image-02]: /images/posts/2026/02/global-power-platform-bootcamp-seoul/global-power-platform-bootcamp-seoul-retro.geunhee.JPG
[image-03]: /images/posts/2026/02/global-power-platform-bootcamp-seoul/global-power-platform-bootcamp-seoul-retro.justin.JPG

[ms]: https://microsoft.com
[officetutor]: https://www.officetutor.co.kr/
[maf]: https://learn.microsoft.com/ko-kr/agent-framework/
[copilot-studio]: https://www.microsoft.com/microsoft-copilot/microsoft-copilot-studio

[gppb-event]: https://event-us.kr/gppb2026seoul/event/119487
[gh-sample]: https://github.com/Azure-Samples/maf-workshop-in-a-day-ko

[session-01]: https://github.com/Azure-Samples/maf-workshop-in-a-day-ko/blob/main/docs/00-setup.md
[session-02]: https://github.com/Azure-Samples/maf-workshop-in-a-day-ko/blob/main/docs/01-single-agent-with-maf.md
[session-03]: https://github.com/Azure-Samples/maf-workshop-in-a-day-ko/blob/main/docs/03-multi-agent-with-maf.md
[session-04]: https://github.com/Azure-Samples/maf-workshop-in-a-day-ko/blob/main/docs/04-aspire-orchestration.md
[session-05]: https://github.com/Azure-Samples/maf-workshop-in-a-day-ko/blob/main/docs/05-mcp-server-development.md
[session-06]: https://github.com/Azure-Samples/maf-workshop-in-a-day-ko/blob/main/docs/06-mcp-server-integration-with-maf.md
[session-07]: https://github.com/Azure-Samples/maf-workshop-in-a-day-ko/blob/main/docs/07-mcp-server-integration-with-copilot-studio.md

[yoojustin-sns]: https://www.linkedin.com/in/justinyoo/
[siyoung-sns]: https://www.linkedin.com/in/krsy0411/
[geunhee-sns]: https://linkedin.com/in/geunhee-kim1227
[jihyeon-sns]: https://www.linkedin.com/in/jihyeon081/
[matdaaiga-sns]: https://www.linkedin.com/company/matdaaiga