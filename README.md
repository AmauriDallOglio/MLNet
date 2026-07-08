# ML.NET 

Microserviço desenvolvido em **.NET 8** para carregamento e execução de modelos de **Machine Learning** utilizando **ML.NET**. O objetivo é disponibilizar uma API REST para realizar inferências de forma rápida, escalável e desacoplada das aplicações consumidoras.

## Funcionalidades

* Carregamento de modelos treinados (`.zip`) durante a inicialização da aplicação.
* Execução de previsões (inferência) através de API REST.
* Gerenciamento dos modelos em memória para maior desempenho.
* Suporte ao versionamento e atualização de modelos.
* Documentação da API com Swagger/OpenAPI.
* Health Checks para monitoramento da aplicação.
* Logging estruturado para auditoria e diagnóstico.
* Pronto para execução em ambientes Docker.

## Tecnologias

* .NET 8
* ASP.NET Core Web API
* ML.NET
* Swagger / OpenAPI
* Serilog
* Docker

##  Estrutura



## Objetivo

Este projeto tem como finalidade centralizar a execução de modelos de Machine Learning em um único serviço, permitindo que diferentes sistemas consumam previsões por meio de uma API, facilitando a manutenção, escalabilidade e evolução dos modelos sem impactar as aplicações clientes.

## Casos de Uso

* Classificação de textos
* Análise de sentimento
* Predição de categorias
* Detecção de fraudes
* Recomendações
* Previsões baseadas em dados estruturados

## Benefícios

* Arquitetura desacoplada.
* Reutilização dos modelos de IA.
* Alta performance com modelos carregados em memória.
* Facilidade para implantação em ambientes Cloud.
* Escalabilidade independente dos sistemas consumidores.

## Próximas Evoluções

* Versionamento automático de modelos.
* Suporte a múltiplos modelos simultaneamente.
* Integração com Azure Machine Learning.
* Monitoramento de métricas de inferência.
* Cache de resultados.
* Autenticação e autorização via JWT.
