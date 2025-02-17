## Intro

Every day, a new AI tool's coming up! 
You're already tired of where to start. 
I'll talk about AI and do live coding

This repository is made for my talk at [BBT Connect: .NET](https://kommunity.com/bursa-bilisim-toplulugu/events/bbt-connect-net-c94ab11a) on 2025-02-08.
 

![Alper Ebicoglu - BBT Connect - AI Talk](https://github.com/user-attachments/assets/eda148c2-623d-4fea-9bb6-2b730ab84a42)

---

## Presentation

👉 [Download the Presentation](https://github.com/ebicoglu/photo-picker/raw/refs/heads/main/presentation.pptx) 



---

## Running this Sample Project

1. Download the application from https://ollama.com/ and install. 
It will install to the following Windows directory
```
%LocalAppData%\Programs\Ollama
```

2. Install the following language model. 
It's an image vision language model.
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

