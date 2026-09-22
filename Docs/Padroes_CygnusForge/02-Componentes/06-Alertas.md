# Alertas e Mensagens de Feedback

Alertas comunicam ao usuario o resultado de uma acao ou informacoes importantes que exigem atencao.

> Ver exemplos visuais: [Exemplos-HTML/alertas.html](../Exemplos-HTML/alertas.html)

---

## Indice

1. [Tipos de Alerta](#1-tipos-de-alerta)
2. [Anatomia de um Alerta](#2-anatomia-de-um-alerta)
3. [Alertas Inline vs Flutuantes](#3-alertas-inline-vs-flutuantes)
4. [Quando Usar Cada Tipo](#4-quando-usar-cada-tipo)
5. [Regras de Uso](#5-regras-de-uso)

---

## 1. Tipos de Alerta

### Success — Sucesso

| Token | Valor |
|-------|-------|
| Fundo | `#D4EDDA` |
| Borda | `#28A745` |
| Texto | `#155724` |

**Quando usar:** Acao concluida com sucesso. Ex: "Cadastro salvo com sucesso.", "Relatorio enviado."

---

### Danger — Erro

| Token | Valor |
|-------|-------|
| Fundo | `#F8D7DA` |
| Borda | `#DC3545` |
| Texto | `#721C24` |

**Quando usar:** Erro que impede a conclusao da acao. Ex: "Nao foi possivel salvar. Verifique os campos em vermelho.", "Erro de conexao."

---

### Warning — Aviso

| Token | Valor |
|-------|-------|
| Fundo | `#FFF3CD` |
| Borda | `#FFC107` |
| Texto | `#856404` |

**Quando usar:** Situacao que exige atencao mas nao impede a acao. Ex: "Existem 3 itens com divergencia. Verifique antes de continuar.", "Este registro sera alterado retroativamente."

---

### Info — Informativo

| Token | Valor |
|-------|-------|
| Fundo | `#D1ECF1` |
| Borda | `#17A2B8` |
| Texto | `#0C5460` |

**Quando usar:** Informacao util ao contexto atual, sem urgencia. Ex: "Os dados sao atualizados automaticamente a cada hora.", "Este modulo requer permissao de gestor."

---

### Primary — Contextual

| Token | Valor |
|-------|-------|
| Fundo | `#F8F9FA` |
| Borda | `#184194` |
| Texto | `#495057` |

**Quando usar:** Orientacoes ou instrucoes para o usuario, sem conotacao de erro ou aviso. Ex: "Preencha os campos abaixo para gerar o relatorio.", notas no topo de formularios complexos.

---

## 2. Anatomia de um Alerta

### Alerta Simples

```
+------------------------------------------+
|  Texto da mensagem de feedback           |
+------------------------------------------+
```

- Padding: 12px todos os lados
- Borda: 1px solid (cor da variante)
- Border radius: 6px
- Fundo: cor clara da variante

### Alerta com Icone

```
+------------------------------------------+
| [Icone]  Texto da mensagem de feedback   |
+------------------------------------------+
```

- Icone: 16px, mesma cor do texto
- Espaco entre icone e texto: 8px

### Alerta com Titulo e Descricao

```
+------------------------------------------+
| [Icone]  Titulo do Alerta                |
|          Descricao mais detalhada sobre  |
|          o que aconteceu.                |
+------------------------------------------+
```

- Titulo: 14px SemiBold
- Descricao: 13px Normal, cor levemente mais suave

### Alerta Dismissivel (fechavel)

```
+------------------------------------------+
|  Texto da mensagem de feedback   [ X ]   |
+------------------------------------------+
```

- Botao X no canto superior direito
- Apenas alertas informativos e de sucesso devem ser fechaveis
- Alertas de erro geralmente nao sao fechaveis (o erro persiste ate ser resolvido)

---

## 3. Alertas Inline vs Flutuantes

### Alerta Inline

Aparece **dentro do fluxo da pagina** — faz parte do layout.

- Usado para: instrucoes, avisos de contexto, estados de listagem vazia com mensagem
- Fica visivel enquanto a situacao persistir
- Nao some automaticamente

```
Pagina:
[ Header ]
[ Alerta de aviso — ex: "Dados desatualizados" ]  <- alerta inline
[ Conteudo da pagina ]
```

### Alerta Flutuante (Toast / Snackbar)

Aparece **sobre o conteudo**, geralmente no canto inferior ou superior da tela. Some automaticamente.

- Usado para: confirmacao de acao realizada com sucesso, erros transitories
- Dura 3-5 segundos antes de desaparecer
- Nao deve exigir interacao do usuario (exceto se tiver botao de acao)

```
                          [ Salvo com sucesso! ]   <- aparece, some em 3s
[ Conteudo da pagina — nao e afetado ]
```

---

## 4. Quando Usar Cada Tipo

| Situacao | Tipo | Estilo |
|----------|------|--------|
| Registro salvo / operacao concluida | Success | Toast (some em 3s) |
| Erro de validacao de formulario | Danger | Inline (persiste) |
| Erro de sistema / conexao | Danger | Inline ou Modal |
| Aviso antes de acao critica | Warning | Inline ou Dialog |
| Instrucoes no topo de formulario | Info ou Primary | Inline (fixo) |
| Divergencias encontradas | Warning | Inline |
| Importacao concluida com falhas parciais | Warning | Inline com detalhes |

---

## 5. Regras de Uso

### Obrigatorio

- Sempre exibir feedback apos uma acao do usuario (salvar, enviar, excluir)
- Mensagens de sucesso devem ser especificas: "Funcionario salvo" e melhor que "Operacao realizada"
- Mensagens de erro devem orientar o que fazer: "CPF invalido. Verifique o formato" em vez de "Erro"

### Evite

- Mais de 2 alertas inline na mesma pagina ao mesmo tempo (priorize)
- Toast de erro que some automaticamente — o usuario pode nao ter tempo de ler
- Usar Warning para erros reais — Warning e para avisos, Danger e para erros
- Alertas genéricos como "Erro" ou "OK" sem contexto

### Textos de alerta

Boas praticas para escrita de mensagens:
- **Sucesso:** "X foi [verbo]" — "Relatorio gerado com sucesso." / "Funcionario cadastrado."
- **Erro:** "Nao foi possivel X porque Y. Para resolver, Z." — quando possivel
- **Aviso:** "Atencao: Y pode acontecer. Deseja continuar?"
- **Info:** Use voz neutra, sem urgencia

---

*Anterior: [05-Tabelas.md](05-Tabelas.md) | Proximo: [07-Tabs.md](07-Tabs.md)*
