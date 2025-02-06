1. Download https://ollama.com/ and install.

2. Download language model `llama3.2-vision`.  
   See all language models https://ollama.com/search

```
cd %LocalAppData%\Programs\Ollama
ollama pull llama3.2-vision
```

* `llama3.2-vision`: 7.9 GB  *(for image vision)*
* `llava`: 4.7 GB *(for image vision)*
* `deepseek-r1`: 4.7 GB
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

