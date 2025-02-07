## Intro

This repository is made for my talk at [BBT Connect: .NET](https://kommunity.com/bursa-bilisim-toplulugu/events/bbt-connect-net-c94ab11a) on 2025-02-08.

---

## Presentation

👉 [Download the Conference Presentation](https://github.com/ebicoglu/photo-picker/raw/refs/heads/main/presentation.pptx) 



---

## Running Sample Project

1. Download https://ollama.com/ and install.

2. Download language model `llama3.2-vision`.  
   See all language models https://ollama.com/search

```
cd %LocalAppData%\Programs\Ollama
ollama pull llama3.2-vision
```

* Popular models:

  * `llama3.3`: 43GB
  * `deepseek-r1`: 4.7 GB
  * `llama3.2-vision`: 7.9 GB  *(for image vision)*
  * `llava`: 4.7 GB *(for image vision)*
  * 
  * `llama3.2`: 2.0 GB

  3. Check if it's loaded at http://localhost:11434/api/tags or 


```
ollama list
```


4. Start Ollama server

```
cd %LocalAppData%\Programs\Ollama
ollama serve
```



---



Similar mobile app [Picker](https://apps.apple.com/us/app/picker-ai-best-photo-picker/id6448671716)

