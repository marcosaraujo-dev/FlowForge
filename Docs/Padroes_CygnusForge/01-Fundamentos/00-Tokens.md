# Referência de Design Tokens

Este arquivo define **todos os tokens** do Design System da Empresa — os nomes oficiais que devem ser usados na documentação, nos estilos CSS e nos recursos XAML.

Nunca use valores hexadecimais diretamente no código ou na documentação. Use sempre o nome do token.

---

## Como usar os tokens

| Contexto | Sintaxe |
|---------|---------|
| **CSS / React** | `var(--color-primary)` |
| **WPF XAML** | `{StaticResource PrimaryColor}` |
| **Documentação** | Escreva o nome do token: `PrimaryColor` |

---

## Cores Primárias

| Token | CSS Variable | WPF Resource | Valor | Descrição |
|-------|-------------|-------------|-------|-----------|
| `PrimaryColor` | `--color-primary` | `PrimaryColor` | `#184194` | Cor principal da marca |
| `PrimaryHoverColor` | `--color-primary-hover` | `PrimaryHoverColor` | `#0F2D6B` | Hover do botão primário |
| `PrimaryDarkColor` | `--color-primary-dark` | `PrimaryDarkColor` | `#0F2D6B` | Variação escura |
| `PrimaryTextColor` | `--color-primary-text` | `PrimaryTextColor` | `#FFFFFF` | Texto sobre fundo primário |

---

## Cores Semânticas — Success

| Token | CSS Variable | WPF Resource | Valor |
|-------|-------------|-------------|-------|
| `SuccessColor` | `--color-success` | `SuccessColor` | `#28A745` |
| `SuccessHoverColor` | `--color-success-hover` | `SuccessHoverColor` | `#218838` |
| `SuccessTextColor` | `--color-success-text` | `SuccessTextColor` | `#FFFFFF` |
| `BadgeSuccessBackground` | `--badge-success-bg` | `BadgeSuccessBackground` | `#D4EDDA` |
| `BadgeSuccessText` | `--badge-success-text` | `BadgeSuccessText` | `#155724` |
| `AlertSuccessBackground` | `--alert-success-bg` | `AlertSuccessBackground` | `#D4EDDA` |
| `AlertSuccessBorder` | `--alert-success-border` | `AlertSuccessBorder` | `#28A745` |
| `AlertSuccessText` | `--alert-success-text` | `AlertSuccessText` | `#155724` |

---

## Cores Semânticas — Danger

| Token | CSS Variable | WPF Resource | Valor |
|-------|-------------|-------------|-------|
| `DangerColor` | `--color-danger` | `DangerColor` | `#DC3545` |
| `DangerHoverColor` | `--color-danger-hover` | `DangerHoverColor` | `#C82333` |
| `DangerTextColor` | `--color-danger-text` | `DangerTextColor` | `#FFFFFF` |
| `BadgeErrorBackground` | `--badge-error-bg` | `BadgeErrorBackground` | `#F8D7DA` |
| `BadgeErrorText` | `--badge-error-text` | `BadgeErrorText` | `#721C24` |
| `AlertDangerBackground` | `--alert-danger-bg` | `AlertDangerBackground` | `#F8D7DA` |
| `AlertDangerBorder` | `--alert-danger-border` | `AlertDangerBorder` | `#DC3545` |
| `AlertDangerText` | `--alert-danger-text` | `AlertDangerText` | `#721C24` |

---

## Cores Semânticas — Warning

| Token | CSS Variable | WPF Resource | Valor |
|-------|-------------|-------------|-------|
| `WarningColor` | `--color-warning` | `WarningColor` | `#FFC107` |
| `WarningHoverColor` | `--color-warning-hover` | `WarningHoverColor` | `#E0A800` |
| `WarningTextColor` | `--color-warning-text` | `WarningTextColor` | `#212529` |
| `BadgeWarningBackground` | `--badge-warning-bg` | `BadgeWarningBackground` | `#FFF3CD` |
| `BadgeWarningText` | `--badge-warning-text` | `BadgeWarningText` | `#856404` |
| `AlertWarningBackground` | `--alert-warning-bg` | `AlertWarningBackground` | `#FFF3CD` |
| `AlertWarningBorder` | `--alert-warning-border` | `AlertWarningBorder` | `#FFC107` |
| `AlertWarningText` | `--alert-warning-text` | `AlertWarningText` | `#856404` |

---

## Cores Semânticas — Info

| Token | CSS Variable | WPF Resource | Valor |
|-------|-------------|-------------|-------|
| `InfoColor` | `--color-info` | `InfoColor` | `#17A2B8` |
| `InfoHoverColor` | `--color-info-hover` | `InfoHoverColor` | `#138496` |
| `InfoTextColor` | `--color-info-text` | `InfoTextColor` | `#FFFFFF` |
| `BadgeInfoBackground` | `--badge-info-bg` | `BadgeInfoBackground` | `#D1ECF1` |
| `BadgeInfoText` | `--badge-info-text` | `BadgeInfoText` | `#0C5460` |
| `AlertInfoBackground` | `--alert-info-bg` | `AlertInfoBackground` | `#D1ECF1` |
| `AlertInfoBorder` | `--alert-info-border` | `AlertInfoBorder` | `#17A2B8` |
| `AlertInfoText` | `--alert-info-text` | `AlertInfoText` | `#0C5460` |

---

## Cores Semânticas — Secondary

| Token | CSS Variable | WPF Resource | Valor |
|-------|-------------|-------------|-------|
| `SecondaryColor` | `--color-secondary` | `SecondaryColor` | `#6C757D` |
| `SecondaryHoverColor` | `--color-secondary-hover` | `SecondaryHoverColor` | `#545B62` |
| `SecondaryTextColor` | `--color-secondary-text` | `SecondaryTextColor` | `#FFFFFF` |

---

## Cores Neutras e de Fundo

| Token | CSS Variable | WPF Resource | Valor |
|-------|-------------|-------------|-------|
| `WhiteColor` | `--color-white` | `WhiteColor` | `#FFFFFF` |
| `BackgroundColor` | `--color-background` | `BackgroundColor` | `#F5F5F5` |
| `SidebarBackgroundColor` | `--color-sidebar-bg` | `SidebarBackgroundColor` | `#F8F9FA` |
| `SurfaceColor` | `--color-surface` | `SurfaceColor` | `#FFFFFF` |
| `HoverBackgroundColor` | `--color-hover-bg` | `HoverBackgroundColor` | `#E9ECEF` |
| `DisabledColor` | `--color-disabled` | `DisabledColor` | `#94A3B8` |
| `DisabledTextColor` | `--color-disabled-text` | `DisabledTextColor` | `#CBD5E1` |
| `InfoBackgroundColor` | `--color-info-bg` | `InfoBackgroundColor` | `#D1ECF1` |

---

## Cores de Borda

| Token | CSS Variable | WPF Resource | Valor |
|-------|-------------|-------------|-------|
| `BorderColor` | `--color-border` | `BorderColor` | `#DEE2E6` |
| `BorderLightColor` | `--color-border-light` | `BorderLightColor` | `#E9ECEF` |
| `BorderHoverColor` | `--color-border-hover` | `BorderHoverColor` | `#CBD5E1` |
| `BorderPressedColor` | `--color-border-pressed` | `BorderPressedColor` | `#93C5FD` |
| `InfoBorderLight` | `--color-info-border-light` | `InfoBorderLight` | `#B3D9FF` |

---

## Cores de Texto

| Token | CSS Variable | WPF Resource | Valor |
|-------|-------------|-------------|-------|
| `TextPrimaryColor` | `--text-primary` | `TextPrimaryColor` | `#495057` |
| `TextSecondaryColor` | `--text-secondary` | `TextSecondaryColor` | `#6C757D` |
| `TextLightColor` | `--text-light` | `TextLightColor` | `#ADB5BD` |
| `TextFieldColor` | `--text-field` | `TextFieldColor` | `#333333` |
| `LinkColor` | `--color-link` | `LinkColor` | `#0066CC` |
| `LinkHoverColor` | `--color-link-hover` | `LinkHoverColor` | `#0052A3` |

---

## Cores de DataGrid

| Token | CSS Variable | WPF Resource | Valor |
|-------|-------------|-------------|-------|
| `DataGridHeaderBackground` | `--datagrid-header-bg` | `DataGridHeaderBackground` | `#F8F9FA` |
| `DataGridRowBorder` | `--datagrid-row-border` | `DataGridRowBorder` | `#E9ECEF` |
| `DataGridRowHoverBrush` | `--datagrid-row-hover` | `DataGridRowHoverBrush` | `#F5F8FF` |
| `DataGridRowSelectedBrush` | `--datagrid-row-selected` | `DataGridRowSelectedBrush` | `#E3EFFF` |
| `DataGridRowHoverSelectedBrush` | `--datagrid-row-hover-selected` | `DataGridRowHoverSelectedBrush` | `#D6E8FF` |

---

## Tokens de Tipografia

| Token | WPF Resource | Valor | Uso |
|-------|-------------|-------|-----|
| `AppFontXSmall` | `AppFontXSmall` | `10` | Texto muito pequeno |
| `AppFontSmall` | `AppFontSmall` | `12` | Texto auxiliar, Caption |
| `AppFontCaption` | `AppFontCaption` | `13` | FieldLabel, texto secundário |
| `AppFontNormal` | `AppFontNormal` | `14` | **Padrão** — corpo de texto |
| `AppFontMedium` | `AppFontMedium` | `16` | Subtítulos |
| `AppFontLarge` | `AppFontLarge` | `18` | H3 |
| `AppFontXLarge` | `AppFontXLarge` | `20` | SectionTitle / H2 |
| `AppFontXXLarge` | `AppFontXXLarge` | `22` | H2 importante |
| `AppFont3XLarge` | `AppFont3XLarge` | `24` | PageTitle |

---

## Tokens de Espaçamento

### Valores (Double)

| Token | Valor |
|-------|-------|
| `Space1` | 4px |
| `Space2` | 8px |
| `Space3` | 12px |
| `Space4` | 16px |
| `Space5` | 20px |
| `Space6` | 24px |
| `Space8` | 32px |

### Padding

| Token | Valor |
|-------|-------|
| `Padding.XSmall` | 4px |
| `Padding.Small` | 8px |
| `Padding.Medium` | 16px |
| `Padding.Large` | 24px |
| `Padding.Button.Default` | 20px H, 10px V |
| `Padding.Card` | 10px |
| `Padding.Input` | 8px H, 6px V |

### Margin

| Token | Valor |
|-------|-------|
| `Margin.Between.Items` | 0,0,0,15px |
| `Margin.Between.Sections` | 0,0,0,20px |
| `Margin.Between.Cards` | 0,0,0,24px |
| `Margin.Label.To.Input` | 0,0,0,5px |
| `Margin.Container` | 30px |

### Corner Radius

| Token | Valor | Uso |
|-------|-------|-----|
| `CornerRadius.None` | 0 | Sem arredondamento |
| `CornerRadius.Small` | 2px | Elementos pequenos |
| `CornerRadius.Medium` | 4px | Inputs, botões |
| `CornerRadius.Large` | 8px | Cards, modais |
| `CornerRadius.Pill` | 10px | Badges |

### Alturas de Componentes

| Token | Valor |
|-------|-------|
| `Height.Button.Small` | 28px |
| `Height.Button.Medium` | 34px |
| `Height.Button.Large` | 40px |
| `Height.Input.Medium` | 34px |
| `Height.DataGrid.Row` | 48px |
| `Height.DataGrid.Header` | 40px |

---

## Estilos de Texto (WPF)

| Token | Fonte | Peso | Cor |
|-------|-------|------|-----|
| `PageTitle` | `AppFont3XLarge` (24px) | SemiBold | `TextPrimaryColor` |
| `SectionTitle` | `AppFontXLarge` (20px) | SemiBold | `TextPrimaryColor` |
| `H2TextStyle` | `AppFontXXLarge` (22px) | SemiBold | `TextPrimaryColor` |
| `H3TextStyle` | `AppFontLarge` (18px) | SemiBold | `TextPrimaryColor` |
| `H4TextStyle` | `AppFontNormal` (14px) | SemiBold | `TextPrimaryColor` |
| `Subtitle` | `AppFontNormal` (14px) | Bold | `TextPrimaryColor` |
| `BodyText` | `AppFontNormal` (14px) | Normal | `TextPrimaryColor` |
| `SecondaryText` | `AppFontCaption` (13px) | Normal | `TextSecondaryColor` |
| `FieldLabel` | `AppFontCaption` (13px) | SemiBold | `TextPrimaryColor` |
| `CaptionText` | `AppFontSmall` (12px) | Normal | `TextLightColor` |

---

*Este arquivo é a fonte da verdade para todos os tokens. Qualquer novo token deve ser adicionado aqui primeiro.*
