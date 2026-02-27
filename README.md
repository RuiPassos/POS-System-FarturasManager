# 🛒 POS-System_FarturasManager

[![C#](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=c-sharp&logoColor=white)](#)
[![SQLite](https://img.shields.io/badge/SQLite-07405E?style=for-the-badge&logo=sqlite&logoColor=white)](#)
[![.NET](https://img.shields.io/badge/.NET-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)](#)
[![Vendus API](https://img.shields.io/badge/API-Vendus-blue?style=for-the-badge)](#)

## 📝 Resumo da Aplicação
O **POS-System_FarturasManager** é uma solução completa de Ponto de Venda (Point of Sale) desenvolvida para gerir as operações diárias de uma rulote de comida de rua (*Street Food*). A aplicação permite desde o registo rápido de produtos em ecrãs táteis até à emissão de faturas certificadas pela Autoridade Tributária através da integração com a API do Vendus.

---

## 🚩 O Problema do Cliente
O proprietário de uma rulote de farturas enfrentava dificuldades críticas na gestão do seu negócio:
* **Lentidão no Atendimento**: O registo manual ou em sistemas genéricos atrasava as filas em momentos de pico de vendas.
* **Conformidade Fiscal**: Necessidade de emitir faturas certificadas com NIF de forma rápida e intuitiva.
* **Falta de Dados Estratégicos**: Dificuldade em visualizar os horários de maior movimento e os produtos com maior margem de saída por turno.

## ✅ A Solução Implantada
Desenvolvi uma solução personalizada que prioriza a **velocidade** e a **fiabilidade**:
1. **Interface Tátil de Alta Performance**: Desenvolvi um sistema de botões grandes e categorizados para um registo de produtos em segundos.
2. **Integração REST API**: Implementei comunicação assíncrona com o serviço Vendus para a geração imediata de documentos fiscais válidos.
3. **Dashboard de Business Intelligence (BI)**: Módulo administrativo que agrupa vendas por hora e gera gráficos estatísticos para otimização de turnos.
4. **Persistência de Dados Local**: Utilização de SQLite para garantir o funcionamento offline e um histórico de vendas detalhado.

---

## 📸 Screenshots do Sistema

| **Interface de Vendas (PDV)** | **Seleção de Pagamento** |
| :---: | :---: |
| <img src="https://github.com/user-attachments/assets/6fcc8d4e-a7bc-46c6-a991-fb79435155b5" width="400"> | <img src="https://github.com/user-attachments/assets/1948f6d8-fd6b-4327-b7f8-418eeda67681" width="400"> |
| *Registo rápido com suporte a NIF* | *Múltiplos métodos de pagamento* |
| **Dashboard Administrativo** | **Gestão de Produtos** |
| <img src="https://github.com/user-attachments/assets/79441d3f-2557-4d3c-b941-119c4b92bfe8" width="400"> | <img src="https://github.com/user-attachments/assets/926bbbe0-6a75-407a-b59d-9f2d222c9564" width="400"> |
| *Análise de faturação por hora* | *CRUD completo de inventário e fotos* |

---

## 🛠️ Stack Tecnológica
* **Linguagem**: C# (.NET Framework)
* **UI**: Windows Forms (WinForms) com geração dinâmica de controlos.
* **Base de Dados**: SQLite (Persistência e Histórico).
* **API**: Integração REST via `HttpClient` (JSON).
* **Bibliotecas**: Newtonsoft.Json, System.Data.SQLite.

## 🚀 Funcionalidades Chave
* **UI Adaptativa**: O ecrã de vendas reconstrói-se automaticamente ao detetar alterações nas categorias ou produtos na base de dados.
* **Validação de Dados Fiscais**: Sistema inteligente de envio de `fiscal_id` para conformidade com a API de faturação.
* **Painel de Pagamento Centralizado**: Suporte para Numerário, Multibanco, Cartão de Crédito e Transferências.
* **Robustez**: Tratamento de exceções para falhas de rede e integridade de dados (ex: tratamento de `DBNull`).

---

## 📂 Estrutura do Projeto

| Ficheiro / Pasta | Descrição |
| :--- | :--- |
| **`Form1.cs`** | Lógica principal da interface de vendas e gestão de checkout. |
| **`VendusAPI.cs`** | Camada de comunicação assíncrona com o motor de faturação. |
| **`FormAdmin.cs`** | Implementação do Dashboard estatístico e gráficos de barras. |
| **`FormGerirProdutos.cs`** | Interface CRUD para gestão de stock, preços e personalização visual. |
| **`ConexaoBD.cs`** | Gestor de ligação e inicialização automática do esquema SQLite. |

---

## 💻 Como Executar
1. **Ambiente**: Ter o .NET Framework e o Visual Studio 2022 instalados.
2. **Clone**: `git clone https://github.com/RuiPassos/POS-System-FarturasManager.git`
3. **Configuração**: 
   * Localiza o ficheiro `VendusAPI.cs`.
   * Insere a tua `ApiKey` na variável correspondente.
4. **Execução**: Prime `F5` para iniciar a aplicação em modo de depuração.

---

### 💡 Nota do Desenvolvedor
O maior desafio técnico deste projeto foi a criação de uma lógica SQL personalizada para agrupar vendas por hora (`strftime('%H', DataHora)`), permitindo consolidar dados de faturação de múltiplos dias no mesmo gráfico de barras, facilitando a análise de performance do negócio.

---
