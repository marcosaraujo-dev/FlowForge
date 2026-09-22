# Animacoes e Transicoes

Animacoes dao feedback visual e guiam a atencao do usuario. Devem ser sutis, rapidas e funcionais — nunca decorativas.

---

## Indice

1. [Principios](#1-principios)
2. [Tokens de duracao](#2-tokens-de-duracao)
3. [Curvas de easing](#3-curvas-de-easing)
4. [Transicoes por componente](#4-transicoes-por-componente)
5. [Animacoes de loading](#5-animacoes-de-loading)
6. [Animacoes de entrada e saida](#6-animacoes-de-entrada-e-saida)
7. [Reducao de movimento](#7-reducao-de-movimento)
8. [Regras](#8-regras)

---

## 1. Principios

| Principio | Descricao |
|-----------|-----------|
| **Funcional** | Toda animacao tem proposito: feedback, orientacao ou transicao. Nunca e puramente estetica |
| **Rapida** | O usuario nunca deve esperar uma animacao terminar para continuar interagindo |
| **Sutil** | Animacoes devem ser quase imperceptiveis — o usuario sente fluidez, nao "efeitos" |
| **Consistente** | Mesma acao = mesma animacao em toda a aplicacao |
| **Respeitosa** | Usuarios que preferem movimento reduzido devem ser atendidos (ver secao 7) |

---

## 2. Tokens de duracao

| Token | Valor | Uso |
|-------|-------|-----|
| `Duration.Instant` | 0ms | Sem animacao (estados que mudam imediatamente) |
| `Duration.Fast` | 100ms | Hover de cor, foco de borda, opacity de icone |
| `Duration.Normal` | 150ms | Transicoes de estado (hover, active), fade de conteudo |
| `Duration.Slow` | 250ms | Expansao/colapso de accordions, slide de paineis |
| `Duration.Slower` | 400ms | Entrada de modais, transicoes de pagina |

### Como escolher a duracao

```
Interacao imediata (cor, borda, opacity)?
    → Duration.Fast (100ms)

Mudanca de estado visivel (hover, pressed, toggle)?
    → Duration.Normal (150ms)

Mudanca de layout (expandir, colapsar, slide)?
    → Duration.Slow (250ms)

Elemento grande entrando/saindo (modal, pagina)?
    → Duration.Slower (400ms)
```

---

## 3. Curvas de easing

| Token | Valor CSS | Uso |
|-------|-----------|-----|
| `Easing.Default` | `ease-out` | Padrao para a maioria das transicoes |
| `Easing.Enter` | `ease-out` | Elementos entrando na tela (chegam rapido, desaceleram) |
| `Easing.Exit` | `ease-in` | Elementos saindo da tela (aceleram ao sair) |
| `Easing.Move` | `ease-in-out` | Elementos se movendo na tela (suavizam inicio e fim) |
| `Easing.Linear` | `linear` | Apenas para animacoes continuas (spinners, progress bars) |

### Por que ease-out e o padrao

O `ease-out` faz o elemento chegar rapido ao destino e desacelerar suavemente. Isso da a sensacao de responsividade — a interface "reage" imediatamente ao usuario.

```
ease-out:     ████████████▓▓▒▒░░   (rapido no inicio, suave no fim)
ease-in:      ░░▒▒▓▓████████████   (lento no inicio, rapido no fim)
ease-in-out:  ░░▒▒▓▓████▓▓▒▒░░    (suave no inicio e no fim)
linear:       ▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓    (velocidade constante)
```

---

## 4. Transicoes por componente

### Estados interativos

| Componente | Propriedade animada | Duracao | Easing |
|-----------|---------------------|---------|--------|
| Botao — hover | `background-color`, `border-color` | Fast (100ms) | Default |
| Botao — pressed | `transform: scale(0.98)` | Fast (100ms) | Default |
| Input — foco | `border-color`, `box-shadow` | Fast (100ms) | Default |
| Link — hover | `color`, `text-decoration` | Fast (100ms) | Default |
| Card — hover | `box-shadow`, `transform: translateY(-2px)` | Normal (150ms) | Default |
| Badge — aparecimento | `opacity`, `transform: scale` | Normal (150ms) | Enter |
| Linha de tabela — hover | `background-color` | Fast (100ms) | Default |
| Item menu — hover | `background-color`, `color` | Fast (100ms) | Default |

### Expandir e colapsar

| Componente | Propriedade animada | Duracao | Easing |
|-----------|---------------------|---------|--------|
| Accordion — expandir | `max-height`, `opacity` | Slow (250ms) | Enter |
| Accordion — colapsar | `max-height`, `opacity` | Slow (250ms) | Exit |
| Menu lateral — expandir | `width` (60px → 240px) | Slow (250ms) | Move |
| Menu lateral — colapsar | `width` (240px → 60px) | Slow (250ms) | Move |
| Submenu — abrir | `max-height`, `opacity` | Slow (250ms) | Enter |
| Dropdown — abrir | `opacity`, `transform: scaleY` | Normal (150ms) | Enter |
| Dropdown — fechar | `opacity`, `transform: scaleY` | Fast (100ms) | Exit |

### Entrada e saida

| Componente | Entrada | Saida |
|-----------|---------|-------|
| Dialog/Modal | Fade in (`opacity 0→1`) + scale (`0.95→1`), Slower (400ms) | Fade out (`opacity 1→0`), Normal (150ms) |
| Toast/Snackbar | Slide in de cima (`translateY -100%→0`), Slow (250ms) | Fade out, Normal (150ms) |
| Tooltip | Fade in (`opacity 0→1`), Normal (150ms), delay 400ms | Fade out, Fast (100ms) |
| Loading overlay | Fade in (`opacity 0→1`), Normal (150ms) | Fade out, Normal (150ms) |
| Conteudo de pagina | Fade in (`opacity 0→1`), Normal (150ms) | Nenhuma (troca imediata) |

---

## 5. Animacoes de loading

### Spinner (rotacao continua)

```
Duracao:  800ms por volta
Easing:   linear (velocidade constante)
Direcao:  sentido horario
Loop:     infinito
```

### Skeleton shimmer (brilho pulsante)

```
Duracao:  1400ms por ciclo
Easing:   linear
Gradiente: #E9ECEF → #F5F5F5 → #E9ECEF (esquerda para direita)
Loop:     infinito
```

### Progress bar (preenchimento)

```
Duracao:  proporcional ao progresso real
Easing:   ease-out (cada incremento)
Cor:      Primary (#184194)
Altura:   4px
```

---

## 6. Animacoes de entrada e saida

### Fade

Componente aparece/desaparece mudando opacidade.

```
Entrada: opacity 0 → 1, Duration.Normal (150ms), Easing.Enter
Saida:   opacity 1 → 0, Duration.Fast (100ms), Easing.Exit
```

Usar para: conteudo de pagina, resultados de busca, alertas inline.

### Slide

Componente desliza de uma direcao.

```
Entrada: translateY(-8px) → translateY(0) + opacity 0 → 1, Duration.Slow (250ms), Easing.Enter
Saida:   translateY(0) → translateY(-8px) + opacity 1 → 0, Duration.Normal (150ms), Easing.Exit
```

Usar para: toasts, drawers, paineis laterais.

### Scale

Componente cresce a partir do centro.

```
Entrada: scale(0.95) → scale(1) + opacity 0 → 1, Duration.Slower (400ms), Easing.Enter
Saida:   scale(1) → scale(0.95) + opacity 1 → 0, Duration.Normal (150ms), Easing.Exit
```

Usar para: dialogs, modais.

---

## 7. Reducao de movimento

Usuarios com sensibilidade a movimento podem configurar `prefers-reduced-motion` no sistema operacional. A aplicacao deve respeitar essa preferencia.

### Comportamento com movimento reduzido

| Com animacao | Com `prefers-reduced-motion` |
|-------------|----------------------------|
| Fade 150ms | Fade 0ms (corte seco) ou opacity imediata |
| Slide 250ms | Aparecer sem deslizar |
| Scale 400ms | Aparecer sem escalar |
| Spinner rotacao | Manter (e funcional, nao decorativo) |
| Skeleton shimmer | Manter (e funcional) |
| Hover cores | Manter (so muda cor, sem movimento) |

**Regra:** Animacoes funcionais (loading, progresso) permanecem. Animacoes de transicao e decoracao sao removidas ou reduzidas a fade instantaneo.

### Implementacao

**CSS:**
```css
@media (prefers-reduced-motion: reduce) {
  *, *::before, *::after {
    animation-duration: 0.01ms !important;
    transition-duration: 0.01ms !important;
  }
}
```

**WPF:**
```csharp
bool reduceMotion = SystemParameters.ClientAreaAnimation == false;
```

---

## 8. Regras

| Regra | Descricao |
|-------|-----------|
| Nunca bloquear interacao | O usuario pode clicar/navegar durante qualquer animacao |
| Saida mais rapida que entrada | Saidas usam Duration.Fast ou Normal. Entradas podem ser Slow/Slower |
| Nunca animar cor de texto | Apenas fundo, borda, sombra e opacidade. Texto muda instantaneamente |
| Nao animar mudanca de dados | Quando um valor muda (ex: contador), a mudanca e imediata. Apenas o container pode ter fade |
| Hover nao tem delay | Resposta imediata ao mouse. Tooltip e a unica excecao (delay 400ms para evitar flickering) |
| Consistencia de duracao | Todos os hovers usam Fast, todos os colapsos usam Slow. Nunca misturar duracoes para a mesma categoria |
| Sem bounce/elastic | Easing do tipo "bounce" ou "elastic" nao faz parte do DS. Manter sobriedade |
