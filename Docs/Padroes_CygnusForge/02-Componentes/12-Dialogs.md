# Dialogs e Modais

Dialogs sao janelas sobrepostas que exigem interacao do usuario antes de retornar ao fluxo principal.

> Implementacao tecnica: [WPF/](../WPF/) | [React/](../React/)

---

## Indice

1. [Tipos de Dialog](#1-tipos-de-dialog)
2. [Anatomia](#2-anatomia)
3. [Tamanhos](#3-tamanhos)
4. [Comportamento](#4-comportamento)
5. [Regras de Uso](#5-regras-de-uso)

---

## 1. Tipos de Dialog

### Confirmacao

Solicita confirmacao antes de executar uma acao — especialmente acoes destrutivas ou irreversiveis.

**Uso:** "Deseja excluir este registro?", "Confirmar envio da folha?"

### Formulario

Permite edicao ou cadastro em um contexto modal, sem navegar para outra pagina.

**Uso:** Adicionar item a lista, editar dados simples, configuracoes rapidas.

### Informativo / Alerta

Exibe informacao importante que exige reconhecimento do usuario.

**Uso:** Erros criticos, termos de uso, avisos antes de operacao de risco.

### Wizard Modal

Fluxo passo-a-passo dentro de um modal. Util para onboarding ou configuracao guiada.

**Uso:** Configuracao inicial, importacao de dados em etapas.

---

## 2. Anatomia

```
+--[Overlay escuro semitransparente]-----------+
|                                              |
|   +--[Dialog]----------------------------+   |
|   |  Titulo do Dialog           [ X ]   |   |  <- Header: titulo + fechar
|   |--------------------------------------|   |
|   |                                      |   |
|   |  Conteudo do dialog                  |   |  <- Body: conteudo principal
|   |  Texto, formulario, etc              |   |
|   |                                      |   |
|   |--------------------------------------|   |
|   |  [Cancelar]          [Acao Primaria] |   |  <- Footer: acoes alinhadas a direita
|   +--------------------------------------+   |
|                                              |
+----------------------------------------------+
```

### Especificacoes

- Overlay: `rgba(0,0,0,0.5)` cobrindo toda a tela
- Fundo do dialog: Branco
- Border radius: 8px
- Sombra: `0 8px 32px rgba(0,0,0,0.2)`
- Header: padding 16-20px, borda inferior 1px
- Body: padding 20-24px
- Footer: padding 16px, borda superior 1px, acoes alinhadas a direita

---

## 3. Tamanhos

| Tamanho | Largura | Uso |
|---------|---------|-----|
| **Small** | 400px | Confirmacao simples, mensagem breve |
| **Medium** | 560px | **Padrao** — formulario com poucos campos |
| **Large** | 720px | Formulario extenso, visualizacao de detalhe |
| **Full (Drawer)** | 50-80% da largura | Edicao complexa, formulario longo |

---

## 4. Comportamento

### Fechar

O dialog pode ser fechado por:
- Botao X no canto superior direito
- Botao "Cancelar" no footer
- Clicar no overlay (apenas para dialogs informativos — nao para formularios)
- Tecla ESC

**Atencao:** Para formularios com dados preenchidos, confirme antes de fechar ("Tem certeza? Os dados nao salvos serao perdidos.")

### Foco

Ao abrir, o foco vai para o primeiro campo interativo (formularios) ou para o botao de acao principal (confirmacao).

### Bloqueio de scroll

O scroll da pagina de fundo deve ser bloqueado enquanto o dialog estiver aberto.

---

## 5. Regras de Uso

### Obrigatorio

- Sempre ter ao menos um botao de fechar (X ou "Cancelar")
- Footer alinha acoes a direita: [Cancelar] [Acao Principal]
- Dialogs de confirmacao Danger: botao de confirmacao e Danger, botao de cancelar e secundario

### Use dialogs para

- Acoes que precisam de confirmacao explicitamente
- Edicoes rapidas que nao justificam navegar para outra pagina
- Exibir detalhes sem perder o contexto da lista

### Evite dialogs para

- Fluxos complexos com muitas etapas (prefira uma pagina dedicada)
- Exibir grandes quantidades de dados (prefira uma pagina de detalhe)
- Dialogs dentro de dialogs (no maximo 1 nivel de modal)
- Abrir dialogs automaticamente sem interacao do usuario (exceto erros criticos)

---

*Anterior: [11-Menu-Lateral.md](11-Menu-Lateral.md) | Proximo: [../03-Layout/01-Estrutura-Paginas.md](../03-Layout/01-Estrutura-Paginas.md)*
