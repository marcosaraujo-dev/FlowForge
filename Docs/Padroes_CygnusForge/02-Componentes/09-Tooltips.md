# Tooltips

Tooltips exibem informacoes complementares quando o usuario passa o mouse sobre um elemento.

---

## Quando usar Tooltips

Use tooltips para:
- **Botoes de icone** sem texto — descrever a acao
- **Campos com restricao de tamanho** — exibir o texto completo truncado
- **Termos tecnicos** — explicar siglas ou conceitos
- **Informacoes adicionais** — complementar sem poluir a interface

**Nao use tooltips para:**
- Informacoes essenciais para completar a tarefa (o usuario pode nao ver o tooltip)
- Mensagens de erro (use inline error em vez disso)
- Conteudo muito longo (mais de 2-3 linhas)

---

## Anatomia

### Tooltip Simples

```
            +----------------------+
            | Texto da dica        |
            +----------------------+
              ^
[ Elemento ]
```

- Fundo: escuro `#1A1A1A` ou Primary `#184194`
- Texto: branco, 12-13px
- Border radius: 4-6px
- Padding: 6px 10px
- Seta apontando para o elemento

### Tooltip com Titulo (Rich Tooltip)

```
            +----------------------+
            | Titulo em negrito    |
            | Descricao mais       |
            | detalhada.           |
            +----------------------+
              ^
[ Elemento ]
```

Usado em botoes de acao importantes onde o usuario pode se beneficiar de mais contexto.

---

## Posicionamento

| Posicao | Quando usar |
|---------|------------|
| **Acima** | Default — quando ha espaco suficiente |
| **Abaixo** | Quando o elemento esta proximo ao topo da tela |
| **Direita** | Para elementos na extremidade esquerda |
| **Esquerda** | Para elementos na extremidade direita |

O tooltip deve ser reposicionado automaticamente se a posicao padrao causar corte na tela.

---

## Atraso de Exibicao

- **Atraso de entrada:** 400-600ms — evita tooltips piscando ao mover o mouse
- **Atraso de saida:** 100ms — permite mover o mouse para o tooltip sem ele fechar

---

## Regras de Uso

- Todo botao de icone sem texto DEVE ter tooltip
- O texto do tooltip deve ser conciso (5-15 palavras idealmente)
- Tooltips nao devem ter elementos interativos (links, botoes) — para isso use um popover
- Nao use tooltips em dispositivos touch (o hover nao existe em mobile/tablet)

---

*Anterior: [08-Acordeoes.md](08-Acordeoes.md) | Proximo: [10-Loading-Estados.md](10-Loading-Estados.md)*
