### Fazendo Uma Requisição HTTP Get ###

Você está desenvolvendo um aplicativo para uma imobiliária,
cujo objetivo é agendar visitas dos candidatos a compradores
aos imóveis disponíveis.

Ao iniciar a aplicação, você deve realizar uma requisição
HTTP Get a um serviço fictício na url fictícia "http://aluraimoveis.com.br/imoveis",
utilizando a biblioteca `Microsoft.Net.Http`.

Assinale a alternativa com o código C# correto para realizar
essa operação.

a. ERRADO
```
const string URL_GET_IMOVEIS = "http://aluraimoveis.com.br/imoveis";
HttpClient cliente = new HttpClient();
var resultado = cliente.Get(URL_GET_IMOVEIS);
```

b. CORRETO
```
const string URL_GET_IMOVEIS = "http://aluraimoveis.com.br/imoveis";
HttpClient cliente = new HttpClient();
var resultado = cliente.GetStringAsync(URL_GET_IMOVEIS);
```

c. ERRADO
```
const string URL_GET_IMOVEIS = "http://aluraimoveis.com.br/imoveis";
HttpClient cliente = new HttpClient();
var resultado = await cliente.Get(URL_GET_IMOVEIS);
```

d. CORRETO
```
const string URL_GET_IMOVEIS = "http://aluraimoveis.com.br/imoveis";
HttpClient cliente = new HttpClient();
var resultado = await cliente.GetStringAsync(URL_GET_IMOVEIS);
```

e. ERRADO
```
const string URL_GET_IMOVEIS = "http://aluraimoveis.com.br/imoveis";
var resultado = new HttpClient(URL_GET_IMOVEIS);
```

> OPINIÃO DA ALURA:
> =================
> 
> O método `GetStringAsync` da classe `System.Net.Http.HttpClient` 
> envia uma requisição GET para a Uri especificada e retorna o
> corpo da resposta como uma string numa operação assíncrona.

### Convertendo Json Para Objetos C# ###

Ao fazer uma uma requisição HTTP Get a um serviço fictício 
na url fictícia "http://aluraimoveis.com.br/imoveis", você
recebe uma string com o seguinte resultado:

```
var resultado = await cliente.GetStringAsync(URL_GET_IMOVEIS);
```

```
[{"nome":"Apto 2 dorms Botafogo R Janeiro","preco":485000},
{"nome":"Casa 2 dorms Pituba Bahia","preco":735000},
{"nome":"Flat 2 dorms Moema S Paulo","preco":552000},
{"nome":"Casa 3 dorms Jaçanã S Paulo","preco":422000},
{"nome":"Apto 3 dorms Pampulha MG","preco":511000},
{"nome":"Apto 2 dorms Morumbi S Paulo","preco":453000},
{"nome":"Flat Vl Mariana S Paulo","preco":539000},
{"nome":"Apto 2 dorms Bela Vista Porto Alegre","preco":537000},
{"nome":"Sobrado 2 dorms Guarulhos S Paulo","preco":390000},
{"nome":"Casa 2 dorms Santana S Paulo","preco":457000},
{"nome":"Apto 2 dorms Leblon R Janeiro","preco":899000},
{"nome":"Flat Cruzeiro Belo Horizonte","preco":495000},
{"nome":"Casa 3 dorms Itaigara Salvador","preco":880000}]
```

Cada imóvel deve ser convertido para uma classe `Imovel`:

```
class Imovel
{
    public string nome { get; set; }
    public int preco { get; set; }
}
```

Escreva o código necessário para converter de forma simples a
string acima para objetos da classe `Imovel`.

> OPINIÃO DA ALURA:
> =================
> 
> A string retornada pela chamada HTTP Get ao serviço está no formato
> JSON, portanto devemos utilizar alguma biblioteca JSON para converter
> o resultado em código C#.
> 
> Utilizando a bilbioteca NewtonSoft.JSON, podemos usar o método
> `DeserializeObject` da classe `JsonConvert`, passando como
> argumento a string contendo o texto em JSON e especificando
> que queremos o resultado convertido em um array de objetos
> do tipo `Imovel`:
> 
> ```
> var imoveisJson = JsonConvert.DeserializeObject<Imovel[]>(resultado);
> ```


### Fazendo Uma Requisição HTTP Post ###

Considere o seguinte código C#, desenvolvido numa aplicação
para uma imobiliária, com o objetivo de chamar um serviço
HTTP Rest para salvar os dados de uma visita a um imóvel do 
catálogo, utilizando para isso uma requisição HTTP Post:

```
HttpClient cliente = new HttpClient();

var dataHoraVisita = new DateTime(
    DataVisita.Year, DataVisita.Month, DataVisita.Day,
    HoraVisita.Hours, HoraVisita.Minutes, HoraVisita.Seconds);

var json = JsonConvert.SerializeObject(new
{
    nome = Nome,
    fone = Fone,
    email = Email,
    imovel = Imovel.Nome,
    preco = Imovel.Preco,
    dataVisita = dataHoraVisita
});
```

Escolha a alternativa que contém o código necessário para
realizar a chamada HTTP Post e salvar os dados do agendamento
da visita corretamente.

a. ERRADA
```
var resposta = await cliente.PostAsync(URL_POST_AGENDAMENTO, json);
```

b. ERRADA
```
var resposta = cliente.Post(URL_POST_AGENDAMENTO, json);
```

c. ERRADA
```
var conteudo = new StringContent(json, Encoding.UTF8, "application/json");
var resposta = cliente.PostAsync(URL_POST_AGENDAMENTO, conteudo);
```

d. CORRETA
```
var conteudo = new StringContent(json, Encoding.UTF8, "application/json");
var resposta = await cliente.PostAsync(URL_POST_AGENDAMENTO, conteudo);
```

e. ERRADA
```
var conteudo = new StringContent(json, Encoding.UTF8, "application/json");
var resposta = await cliente.Post(URL_POST_AGENDAMENTO, conteudo);
```

> OPINIÃO DA ALURA:
> =================
> 
> Deve-se fazer uma chamada ao método assíncrono `PostAsync` 
> da classe `HttpClient`, utilizando `await` para aguardar
> a finalização da chamada. O método `PostAsync` recebe como
> argumentos a Uri do serviço e também o conteúdo (HttpContent) 
> contendo a string JSON:
> 
> ```
> var conteudo = new StringContent(json, Encoding.UTF8, "application/json");
> var resposta = await cliente.PostAsync(URL_POST_AGENDAMENTO, conteudo);
> ```

### Indicando Que A Aplicação Está Ocupada ###

Numa aplicação de imobiliária, feita com Xamarin Forms,
ao realizar uma chamada a um serviço HTTP Rest, você nota
que a tela fica em branco por um tempo considerável.

Você imagina que seu cliente poderá entender que a tela em
branco é sinal de algum bug no aplicativo, e para isso você
quer avisá-lo de alguma forma de que a aplicação está aguardando
o processamento de uma requisição.

Assinale a alternativa mais adequada para dar esse feedback
ao usuário.

a. Quando a chamada iniciar, exibir uma página 
com uma imagem de um relógio para indicar que
há uma atividade sendo executada.

b. Quando a chamada iniciar, exibir um controle `Label` 
com um texto contendo uma mensagem para indicar que
há uma atividade sendo executada. Quando a chamad HTTP Get
terminar, ocultar o controle `Label` novamente.

c. Quando a chamada iniciar, exibir um controle `ActivityIndicator` para indicar que
há uma atividade sendo executada. Quando a chamad HTTP Get
terminar, ocultar o controle `ActivityIndicator` novamente.
CORRETA.

d. Quando a chamada iniciar, chamar o método `DisplayAlert`
com uma mensagem para indicar que
há uma atividade sendo executada. Quando a chamad HTTP Get
terminar, chamar o método `DisplayAlert` com outra mensagem
para avisar que o serviço respondeu.

> OPINIÃO DA ALURA:
> =================
> 
> O `ActivityIndicator` é um controle visual, usado para indicar
> que algo está sendo executado.
