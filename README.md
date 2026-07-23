# Módulo de Treinamento com ML.NET

ML.NET é um framework de machine learning de código aberto, criado pela Microsoft para desenvolvedores .NET. Ele permite treinar, avaliar e usar modelos de ML diretamente em C#/.NET.

O ML.NET atua como o motor de aprendizado sobre os dados do sistema ERP inseridos pelos usuários. O objetivo é que o sistema não seja estático: ele aprende com o histórico de cada tenant e refina suas previsões/classificações ao longo do tempo, sempre respeitando o isolamento multi-tenant dos dados. O modelo treinado é salvo em um arquivo binário, que pode ser carregado posteriormente para gerar previsões em produção AutoML

Este módulo faz parte de uma solução multi-tenant de gestão financeira desenvolvida em **.NET** com **SQL Server**. O componente aqui documentado é responsável pelo treinamento e retreinamento de modelos de Machine Learning utilizando **ML.NET**, aplicados a cenários de análise e classificação de dados financeiros dentro do próprio sistema.

## Objetivo

Permitir que o sistema aprenda continuamente a partir dos dados inseridos pelos usuários, mantendo a qualidade do modelo ao longo do tempo sem perder o conhecimento adquirido em treinamentos anteriores.

## Estratégia de Retreinamento

Um dos principais desafios enfrentados no desenvolvimento foi o retreinar o modelo apenas com dados novos, o ML.NET perdia a capacidade de generalizar sobre padrões aprendidos anteriormente.

**Solução adotada:**
- O dataset completo utilizado em cada treinamento é serializado em formato **JSON** e armazenado na coluna `DadosSessoes`, junto ao binário do modelo.
- A cada novo ciclo de treinamento, o dataset histórico completo é recuperado, combinado com os novos dados e reutilizado, garantindo que o modelo seja sempre retreinado sobre o conjunto de dados integral e não apenas sobre o incremento mais recente.
- Essa abordagem evita a degradação de performance do modelo em cenários de aprendizado incremental, sem exigir arquiteturas mais complexas (como aprendizado contínuo ou replay buffers externos).

## Arquitetura

O projeto segue um padrão customizado inspirado em **CQRS** (sem uso de MediatR ou AutoMapper), com separação clara de camadas:

- **Dominio** — entidades e regras de negócio
- **Infraestruture** — acesso a dados e integração com o ML.NET
- **Aplicacao** — orquestração dos casos de uso

## Tecnologias

- .NET
- ML.NET
- SQL Server
- Arquitetura CQRS customizada

## Status

Em desenvolvimento contínuo, com foco atual em refinar o pipeline de retreinamento e a padronização das camadas de injeção de dependência.
