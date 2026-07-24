# Módulo de Treinamento com ML.NET

Este módulo faz parte de uma solução desenvolvida em **.NET** com **SQL Server**, voltada para o treinamento, armazenamento e reutilização de modelos de Machine Learning utilizando **ML.NET**.

O objetivo do módulo é permitir que o sistema aprenda a partir dos dados gerados pela aplicação, treinando modelos capazes de identificar padrões em sessões, perguntas, respostas e demais informações persistidas. Com isso, o sistema deixa de operar apenas com regras estáticas e passa a utilizar modelos treinados sobre dados reais para apoiar classificações, previsões ou sugestões futuras.

## Visão Geral

O **ML.NET** é um framework de Machine Learning open source da Microsoft para aplicações .NET. Ele permite criar, treinar, avaliar, salvar e carregar modelos diretamente em C#, sem necessidade de sair do ecossistema .NET.

Neste projeto, o ML.NET é utilizado como motor de treinamento sobre dados armazenados no banco. O modelo gerado é serializado em formato binário e salvo no SQL Server, permitindo que versões treinadas possam ser recuperadas posteriormente sem necessidade de novo treinamento a cada execução.


## Arquitetura

O projeto segue uma organização em camadas, com um padrão customizado inspirado em **CQRS**, sem uso de MediatR ou AutoMapper.

As principais camadas são:

- **Dominio** — entidades, contratos e regras centrais do negócio.
- **Infraestrutura** — acesso a dados, contextos, mapeamentos e repositórios.
- **Aplicacao** — orquestração dos casos de uso, handlers, requests e responses.
- **Api** — exposição dos endpoints HTTP e configuração da aplicação.

Essa separação mantém o treinamento desacoplado da API e concentra a lógica de caso de uso no handler da camada de aplicação.

## Tecnologias Utilizadas

- .NET 8
- ASP.NET Core
- ML.NET
- Entity Framework Core
- SQL Server
- Arquitetura em camadas
- CQRS customizado

## Objetivo

O módulo de treinamento tem como finalidade:

- Buscar dados históricos da aplicação.
- Transformar os dados em um formato adequado para treinamento.
- Treinar um modelo ML.NET com base nas informações disponíveis.
- Persistir o modelo treinado em banco de dados.
- Controlar versão, data de treinamento e quantidade de registros utilizados.
- Permitir evolução futura para ciclos de retreinamento com reaproveitamento do histórico.

## Fluxo de Treinamento

A rota responsável pelo processo é:

```http
GET /api/MLNet/GerarTreinamento
```

Ao ser acionada, a aplicação executa o handler `GerarTreinamentoHandler`, que coordena o fluxo de treinamento:

1. Verifica se já existe um modelo salvo no banco.
2. Caso exista, recupera o último modelo registrado.
3. Caso não exista, busca as sessões disponíveis no banco de dados.
4. Converte as sessões para um DTO de treinamento.
5. Cria um pipeline ML.NET baseado em processamento de texto.
6. Treina o modelo com os dados disponíveis.
7. Serializa o modelo treinado para um array de bytes.
8. Persiste o modelo no banco com informações de versão e quantidade de registros.

## Pipeline ML.NET

O pipeline atual utiliza os dados das sessões como base de treinamento, considerando principalmente:

- **Pergunta** como entrada textual.
- **RespostaModelo** como rótulo/classificação esperada.

O pipeline é composto por:

| Etapa | Função |
|---|---|
| `FeaturizeText` | Transforma o texto da pergunta em atributos numéricos |
| `MapValueToKey` | Converte a resposta esperada em chave classificável |
| `SdcaMaximumEntropy` | Algoritmo de classificação multiclasse |
| `MapKeyToValue` | Converte a predição de volta para o valor original |

Essa estrutura permite treinar um modelo capaz de associar perguntas a respostas esperadas com base nos padrões encontrados nas sessões armazenadas.

## Estratégia de Persistência do Modelo

O modelo treinado é salvo no banco de dados como um binário, permitindo que a aplicação mantenha o histórico de modelos gerados.

Cada modelo armazenado contém informações como:

- Nome do modelo.
- Dados binários do modelo treinado.
- Data do treinamento.
- Versão.
- Quantidade de registros utilizados no treinamento.

Essa abordagem facilita o versionamento e permite que modelos anteriores sejam consultados ou reutilizados futuramente.

## Estratégia de Retreinamento

Um dos principais pontos de atenção no desenvolvimento é o processo de retreinamento.

Ao treinar um modelo apenas com dados novos, existe o risco de perda de generalização sobre padrões aprendidos anteriormente (*catastrophic forgetting*). Para evitar isso, a estratégia prevista é manter o histórico completo dos dados utilizados no treinamento, combinando dados antigos e novos a cada novo ciclo.

A abordagem planejada consiste em:

1. Armazenar o dataset utilizado no treinamento junto ao modelo.
2. Recuperar o histórico completo no próximo ciclo.
3. Combinar os dados antigos com os novos registros.
4. Retreinar o modelo sobre o conjunto integral.

Com isso, o modelo evolui sem perder o conhecimento acumulado em treinamentos anteriores, evitando degradação de performance comum em cenários de retreinamento incremental simples.


## Status

O módulo está em desenvolvimento contínuo. O foco atual está em:

- Validar o fluxo completo de treinamento.
- Ajustar a persistência e recuperação dos modelos.
- Evoluir a estratégia de retreinamento com histórico completo.
- Melhorar o tratamento de erros nas rotas.
- Padronizar a injeção de dependência e os contratos entre camadas.
