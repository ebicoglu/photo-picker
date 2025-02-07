## Intro

This repository is made for my talk at [BBT Connect: .NET](https://kommunity.com/bursa-bilisim-toplulugu/events/bbt-connect-net-c94ab11a) on 2025-02-08.

![image](https://github.com/user-attachments/assets/4bc730cd-6b2e-4e32-be9f-c51eeb405716)

---

## Presentation

👉 [Download the Conference Presentation](https://github.com/ebicoglu/photo-picker/raw/refs/heads/main/presentation.pptx) 



---

## Running the Sample Project

1. Download application from https://ollama.com/ and install. It will install to `
```
%LocalAppData%\Programs\Ollama
```

2. Install  the language image vision model `llava:7b` . It's  4.7GB.
```
ollama pull llava:7b
```

* Other popular language models:

  * `llama3.3`: 43GB
  * `deepseek-r1`: 4.7 GB
  * `llama3.2-vision`: 7.9 GB  *(for image vision)*


3. Check if it's loaded at http://localhost:11434/api/tags or type the below command:
```
ollama list
```

4. Start Ollama server
```
ollama serve
```

5. Run the PhotoPicker .NET Console app (.NET9 required)

---



A similar mobile app [Picker](https://apps.apple.com/us/app/picker-ai-best-photo-picker/id6448671716)

