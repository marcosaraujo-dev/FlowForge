# Navegacao

Padroes de navegacao entre telas e dentro de uma tela.

---

## Tipos de Navegacao

### Navegacao Principal — Menu Lateral

O menu lateral e o ponto de entrada para as diferentes areas do sistema. Detalhes em [11-Menu-Lateral.md](../02-Componentes/11-Menu-Lateral.md).

### Navegacao Contextual — Breadcrumb

Mostra o caminho percorrido pelo usuario dentro da hierarquia de telas.

```
Inicio > Funcionarios > Joao da Silva > Historico de Ferias
```

**Quando usar:** Quando o usuario pode estar em 3 ou mais niveis de profundidade.

**Especificacoes:**
- Separador: `>` ou `/`
- Fonte: 13px, Text Secondary
- Link: Primary, sublinhado no hover
- Item atual (ultimo): Text Primary, sem link

---

### Navegacao por Abas — Tabs

Para alternar entre secoes de uma mesma tela. Detalhes em [07-Tabs.md](../02-Componentes/07-Tabs.md).

---

## Padroes de Transicao

### Transicao suave

Ao navegar entre telas, o conteudo troca sem animacoes abruptas. Uma transicao sutil (fade ou slide) melhora a percepcao de fluidez.

- **Fade:** 150-200ms — para trocas de conteudo simples
- **Slide:** 200-300ms — para abertura de drawers e panels

### Sem transicao

Para operacoes de alta frequencia (filtros, paginacao) a troca e imediata para nao criar latencia percebida.

---

## Estado Ativo

O sistema sempre indica onde o usuario esta:
- Item do menu lateral marcado como ativo
- Breadcrumb mostrando o caminho
- Titulo da pagina correspondendo ao item de menu

Nunca deixe o usuario sem orientacao de onde ele esta no sistema.

---

## Padroes de Acao Pos-Operacao

Apos concluir uma acao, o sistema deve navegar o usuario para um lugar que faz sentido:

| Acao | Navegacao sugerida |
|------|-------------------|
| Criar registro | Ir para a pagina de detalhe do registro criado, ou voltar a lista |
| Editar registro | Voltar ao detalhe ou a lista, com alerta de sucesso |
| Excluir registro | Voltar a lista |
| Cancelar formulario | Voltar a origem (detalhe ou lista) |
| Completar wizard | Ir para o resultado ou para a lista |

---

*Anterior: [01-Estrutura-Paginas.md](01-Estrutura-Paginas.md)*
