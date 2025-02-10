# matdaAIga.kr

matdaAIga 웹사이트는 .NET 기반의 [Statiq](https://www.statiq.dev/) 정적 웹사이트 생성도구를 이용해서 만들었으며, [https://matdaAIga.kr](https://matdaaiga.kr)에서 확인할 수 있습니다.

## 사전 요구사항

- [.NET SDK 9+](https://dotnet.microsoft.com/download/dotnet/9.0)
- [Visual Studio 2022](https://visualstudio.microsoft.com/vs/) 또는 [Visual Studio Code](https://code.visualstudio.com/) + [C# DevKit](https://marketplace.visualstudio.com/items?itemName=ms-dotnettools.csdevkit)
- [GitHub CLI](https://cli.github.com)
- 윈도우 사용자: [PowerShell 7+](https://learn.microsoft.com/powershell/scripting/install/installing-powershell)

## 시작하기

1. 저장소를 자신의 로컬 컴퓨터로 클론합니다.

    ```bash
    gh repo fork matdaaiga-kr/matdaaiga-kr.github.io --clone
    ```

1. 저장소 디렉토리로 이동합니다.

    ```bash
    cd matdaaiga-kr.github.io
    ```

1. 기본 테마를 추가합니다.

    ```bash
    git clone https://github.com/statiqdev/CleanBlog.git ./src/MatdaAIga.Generator/theme
    ```

1. 프로젝트를 빌드하고 실행합니다.

    ```bash
    dotnet restore && dotnet build && dotnet run --project ./src/MatdaAIga.Generator -- preview
    ```

1. 웹 브라우저에서 [http://localhost:5080](http://localhost:5080)으로 접속한 후 웹사이트를 확인합니다.

1. 배포를 위해 아티팩트를 생성하려면 아래 명령어를 실행합니다.

    ```bash
    dotnet run --project ./src/MatdaAIga.Generator -- deploy
    ```

## 컨텐츠 작성 및 수정하기

모든 컨텐츠는 `src/MatdaAIga.Generator/input` 디렉토리에 위치합니다.

### 페이지 컨텐츠

- `src/MatdaAIga.Generator/input/pages` 디렉토리에 새로운 Markdown 파일을 추가하면 새로운 페이지를 만들 수 있습니다. 기존의 파일을 참고해서 만들어 보세요.

### 블로그 컨텐츠

- `src/MatdaAIga.Generator/input/posts` 디렉토리에 새로운 Markdown 파일을 추가하면 새로운 페이지를 만들 수 있습니다. 기존의 파일을 참고해서 만들어 보세요.

### 이미지 컨텐츠

- 페이지에 사용하는 이미지 컨텐츠는 `src/MatdaAIga.Generator/input/images/pages` 디렉토리에 추가합니다.
- 블로그에 사용하는 이미지 컨텐츠는 `src/MatdaAIga.Generator/input/images/posts` 디렉토리에 추가합니다.

## 트러블슈팅

### MacOS 트러블슈팅

MacOS(실리콘 맥)에서 실행시 아래와 같은 에러메시지가 발생할 수 있습니다.

```bash
Unable to load shared library 'libsass' or one of its dependencies
```

이는 `libsass.dylib` 파일이 없어서 발생하는 문제입니다. 아래 명령어를 실행하여 해결할 수 있습니다.

```bash
sudo mkdir -p /usr/local/lib/ && sudo cp ./lib/libsass.dylib "$_"
```

> `lib` 디렉토리에 있는 `libsass.dylib` 파일은 실리콘 맥OS 용으로 새롭게 빌드한 파일입니다.

이후, 다시 `dotnet run --project ./src/MatdaAIga.Generator -- preview` 명령어를 실행하면 정상적으로 실행됩니다. 이 때 보안 경고가 발생할 수 있습니다. 이 경우, `시스템 환경설정` > `보안 및 개인 정보 보호` > `보안` 메뉴에서 `libsass.dylib` 파일을 열 수 있도록 허용해주세요.

![libsass.png](./images/libsass.png)

좀 더 자세한 내용은 [https://github.com/Taritsyn/LibSassHost](https://github.com/Taritsyn/LibSassHost?tab=readme-ov-file#installation)를 참고하세요.

## 웹사이트에 문제가 있나요?

문제가 있거나 개선할 점이 있다면 [이슈](../../issues)를 등록해주세요.
