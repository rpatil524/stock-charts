using WebApi.Models;

namespace WebApi.Services;

public static class MetadataHelpers
{
    public static ChartConfig GetOscillatorConfig(float min = 0, float max = 100, float upperThreshold = 80, float lowerThreshold = 20)
        => new() {
            MinimumYAxis = min,
            MaximumYAxis = max,
            Thresholds = [
                new() {
                    Value = upperThreshold,
                    Color = ChartColors.ThresholdRed,
                    Style = "dash",
                    Fill = new() {
                        Target = "+2",
                        ColorAbove = "transparent",
                        ColorBelow = ChartColors.ThresholdGreen
                    }
                },
                new() {
                    Value = lowerThreshold,
                    Color = ChartColors.ThresholdGreen,
                    Style = "dash",
                    Fill = new() {
                        Target = "+1",
                        ColorAbove = ChartColors.ThresholdRed,
                        ColorBelow = "transparent"
                    }
                }
            ]
        };

    public static List<IndicatorResultConfig> GetPriceBandResults(string name, string? color = null)
    {
        color ??= ChartColors.StandardOrange;
        return [
            new IndicatorResultConfig {
                DisplayName = "Upper Band",
                TooltipTemplate = $"{name} Upper Band",
                DataName = "upperBand",
                DataType = "number",
                LineType = "solid",
                LineWidth = 1,
                DefaultColor = color,
                Fill = new() {
                    Target = "+2",
                    ColorAbove = ChartColors.DarkGrayTransparent,
                    ColorBelow = ChartColors.DarkGrayTransparent
                }
            },
            new() {
                DisplayName = "Centerline",
                TooltipTemplate = $"{name} Centerline",
                DataName = "centerline",
                DataType = "number",
                LineType = "dash",
                LineWidth = 1,
                DefaultColor = color
            },
            new() {
                DisplayName = "Lower Band",
                TooltipTemplate = $"{name} Lower Band",
                DataName = "lowerBand",
                DataType = "number",
                LineType = "solid",
                LineWidth = 1,
                DefaultColor = color
            }
        ];
    }
}

public static class Metadata
{
    public static IEnumerable<IndicatorListing> IndicatorListing(string baseUrl)
    {
        List<IndicatorListing> listing = [
            // Accumulation Distribution Line (ADL)
            new() {
                Name = "Accumulation Distribution Line (ADL)",
                Uiid = "ADL",
                LegendTemplate = "ADL w/ SMA([P1])",
                Endpoint = $"{baseUrl}/ADL/",
                Category = "volume-based",
                ChartType = "oscillator",
                Parameters = [
                    new() {
                        DisplayName = "SMA Periods",
                        ParamName = "smaPeriods",
                        DataType = "int",
                        DefaultValue = 3,
                        Minimum = 1,
                        Maximum = 50
                    }
                ],
                Results = [
                    new() {
                        DisplayName = "ADL",
                        TooltipTemplate = "ADL",
                        DataName = "adl",
                        DataType = "number",
                        LineType = "solid",
                        DefaultColor = ChartColors.StandardBlue
                    },
                    new() {
                        DisplayName = "SMA of ADL",
                        TooltipTemplate = "ADL SMA([P1])",
                        DataName = "adlSma",
                        DataType = "number",
                        LineType = "solid",
                        DefaultColor = ChartColors.StandardRed
                    }
                ]
            },

            // Average Directional Index (ADX)
            new IndicatorListing {
                Name = "Average Directional Index (ADX)",
                Uiid = "ADX",
                LegendTemplate = "ADX([P1])",
                Endpoint = $"{baseUrl}/ADX/",
                Category = "price-trend",
                ChartType = "oscillator",
                ChartConfig = new ChartConfig {
                    Thresholds =
                    [
                        new() {
                            Value = 40,
                            Color = ChartColors.ThresholdGrayTransparent,
                            Style = "dash"
                        },
                        new() {
                            Value = 20,
                            Color = ChartColors.ThresholdGrayTransparent,
                            Style = "dash"
                        }
                    ]
                },
                Parameters =
                [
                    new() {
                        DisplayName = "Lookback Periods",
                        ParamName = "lookbackPeriods",
                        DataType = "int",
                        DefaultValue = 14,
                        Minimum = 2,
                        Maximum = 250
                    }
                ],
                Results = [
                    new() {
                        DisplayName = "ADX",
                        TooltipTemplate = "ADX([P1])",
                        DataName = "adx",
                        DataType = "number",
                        LineType = "solid",
                        DefaultColor = ChartColors.StandardBlue
                    },
                    new() {
                        DisplayName = "DI+",
                        TooltipTemplate = "DI+([P1])",
                        DataName = "pdi",
                        DataType = "number",
                        LineType = "solid",
                        DefaultColor = ChartColors.StandardGreen
                    },
                    new() {
                        DisplayName = "DI-",
                        TooltipTemplate = "DI-([P1])",
                        DataName = "mdi",
                        DataType = "number",
                        LineType = "solid",
                        DefaultColor = ChartColors.StandardRed
                    },
                    new() {
                        DisplayName = "ADX Rating",
                        TooltipTemplate = "ADXR([P1])",
                        DataName = "adxr",
                        DataType = "number",
                        LineType = "solid",
                        LineWidth = 2,
                        DefaultColor = ChartColors.StandardGrayTransparent
                    }
                ]
            },

            // Arnaud Legoux Moving Average (ALMA)
            new IndicatorListing {
                Name = "Arnaud Legoux Moving Average (ALMA)",
                Uiid = "ALMA",
                LegendTemplate = "ALMA([P1],[P2],[P3])",
                Endpoint = $"{baseUrl}/ALMA/",
                Category = "moving-average",
                ChartType = "overlay",
                Parameters =
                [
                    new() {
                        DisplayName = "Lookback Periods",
                        ParamName = "lookbackPeriods",
                        DataType = "int",
                        DefaultValue = 9,
                        Minimum = 2,
                        Maximum = 250
                    },
                    new() {
                        DisplayName = "Offset",
                        ParamName = "offset",
                        DataType = "number",
                        DefaultValue = 0.85,
                        Minimum = 0,
                        Maximum = 1
                    },
                    new() {
                        DisplayName = "Sigma",
                        ParamName = "sigma",
                        DataType = "number",
                        DefaultValue = 6,
                        Minimum = 0.1,
                        Maximum = 10
                    }
                ],
                Results = [
                    new() {
                        DisplayName = "ALMA",
                        TooltipTemplate = "ALMA([P1],[P2],[P3])",
                        DataName = "alma",
                        DataType = "number",
                        LineType = "solid",
                        DefaultColor = ChartColors.StandardBlue
                    }
                ]
            },

            // Aroon Up/Down
            new IndicatorListing {
                Name = "Aroon Up/Down",
                Uiid = "AROON UP/DOWN",
                LegendTemplate = "AROON([P1]) Up/Down",
                Endpoint = $"{baseUrl}/AROON/",
                Category = "price-trend",
                ChartType = "oscillator",
                ChartConfig = new ChartConfig {
                    MinimumYAxis = 0,
                    MaximumYAxis = 100,

                    Thresholds =
                    [
                        new() {
                            Value = 70,
                            Color = ChartColors.ThresholdGrayTransparent,
                            Style = "solid"
                        },
                        new() {
                            Value = 50,
                            Color = ChartColors.ThresholdGrayTransparent,
                            Style = "dash"
                        },
                        new() {
                            Value = 30,
                            Color = ChartColors.ThresholdGrayTransparent,
                            Style = "solid"
                        }
                    ]
                },
                Parameters =
                [
                    new() {
                        DisplayName = "Lookback Periods",
                        ParamName = "lookbackPeriods",
                        DataType = "int",
                        DefaultValue = 25,
                        Minimum = 1,
                        Maximum = 250
                    }
                ],
                Results = [
                    new() {
                        DisplayName = "Aroon Up",
                        TooltipTemplate = "Aroon Up",
                        DataName = "aroonUp",
                        DataType = "number",
                        LineType = "solid",
                        DefaultColor = ChartColors.StandardGreen
                    },
                    new() {
                        DisplayName = "Aroon Down",
                        TooltipTemplate = "Aroon Down",
                        DataName = "aroonDown",
                        DataType = "number",
                        LineType = "solid",
                        DefaultColor = ChartColors.StandardRed
                    }
                ]
            },

            // Aroon Oscillator
            new IndicatorListing {
                Name = "Aroon Oscillator",
                Uiid = "AROON OSC",
                LegendTemplate = "AROON([P1]) Oscillator",
                Endpoint = $"{baseUrl}/AROON/",
                Category = "price-trend",
                ChartType = "oscillator",
                ChartConfig = new ChartConfig {
                    MinimumYAxis = -100,
                    MaximumYAxis = 100,

                    Thresholds =
                    [
                        new() {
                            Value = 0,
                            Color = ChartColors.ThresholdGrayTransparent,
                            Style = "dash"
                        }
                    ]
                },
                Parameters =
                [
                    new() {
                        DisplayName = "Lookback Periods",
                        ParamName = "lookbackPeriods",
                        DataType = "int",
                        DefaultValue = 25,
                        Minimum = 1,
                        Maximum = 250
                    }
                ],
                Results = [
                    new() {
                        DisplayName = "Oscillator",
                        TooltipTemplate = "AROON([P1]) Oscillator",
                        DataName = "oscillator",
                        DataType = "number",
                        LineType = "solid",
                        DefaultColor = ChartColors.StandardBlue
                    }
                ]
            },

            // ATR Trailing Stop (Close offset)
            new IndicatorListing {
                Name = "ATR Trailing Stop (Close offset)",
                Uiid = "ATR-STOP-CLOSE",
                LegendTemplate = "ATR-STOP([P1],[P2],CLOSE)",
                Endpoint = $"{baseUrl}/ATR-STOP-CLOSE/",
                Category = "price-trend",
                ChartType = "overlay",
                Order = Order.Front,
                Parameters =
                [
                    new() {
                        DisplayName = "Lookback Periods",
                        ParamName = "lookbackPeriods",
                        DataType = "int",
                        DefaultValue = 21,
                        Minimum = 1,
                        Maximum = 50
                    },
                    new() {
                        DisplayName = "Multiplier",
                        ParamName = "multiplier",
                        DataType = "number",
                        DefaultValue = 3,
                        Minimum = 0.1,
                        Maximum = 10
                    }
                ],
                Results = [
                    new() {
                        DisplayName = "Buy Stop",
                        TooltipTemplate = "ATR-STOP([P1],[P2],CLOSE) Buy Stop",
                        DataName = "buyStop",
                        DataType = "number",
                        LineType = "dots",
                        LineWidth = 2,
                        DefaultColor = ChartColors.StandardGreen
                    },
                    new() {
                        DisplayName = "Sell Stop",
                        TooltipTemplate = "ATR-STOP([P1],[P2],CLOSE) Sell Stop",
                        DataName = "sellStop",
                        DataType = "number",
                        LineType = "dots",
                        LineWidth = 2,
                        DefaultColor = ChartColors.StandardRed
                    }
                ]
            },

            // ATR Trailing Stop (High/Low offset)
            new IndicatorListing {
                Name = "ATR Trailing Stop (High/Low offset)",
                Uiid = "ATR-STOP-HL",
                LegendTemplate = "ATR-STOP([P1],[P2],HIGH/LOW)",
                Endpoint = $"{baseUrl}/ATR-STOP-HL/",
                Category = "price-trend",
                ChartType = "overlay",
                Order = Order.Front,
                Parameters =
                [
                    new() {
                        DisplayName = "Lookback Periods",
                        ParamName = "lookbackPeriods",
                        DataType = "int",
                        DefaultValue = 21,
                        Minimum = 1,
                        Maximum = 50
                    },
                    new() {
                        DisplayName = "Multiplier",
                        ParamName = "multiplier",
                        DataType = "number",
                        DefaultValue = 3,
                        Minimum = 0.1,
                        Maximum = 10
                    }
                ],
                Results = [
                    new() {
                        DisplayName = "Buy Stop",
                        TooltipTemplate = "ATR-STOP([P1],[P2],HIGH/LOW) Buy Stop",
                        DataName = "buyStop",
                        DataType = "number",
                        LineType = "dots",
                        LineWidth = 2,
                        DefaultColor = ChartColors.StandardGreen
                    },
                    new() {
                        DisplayName = "Sell Stop",
                        TooltipTemplate = "ATR-STOP([P1],[P2],HIGH/LOW) Sell Stop",
                        DataName = "sellStop",
                        DataType = "number",
                        LineType = "dots",
                        LineWidth = 2,
                        DefaultColor = ChartColors.StandardRed
                    }
                ]
            },

            // Average True Range
            new IndicatorListing {
                Name = "Average True Range (ATR)",
                Uiid = "ATR",
                LegendTemplate = "ATR([P1])",
                Endpoint = $"{baseUrl}/ATR/",
                Category = "price-characteristic",
                ChartType = "oscillator",
                Parameters =
                [
                    new() {
                        DisplayName = "Lookback Periods",
                        ParamName = "lookbackPeriods",
                        DataType = "int",
                        DefaultValue = 14,
                        Minimum = 2,
                        Maximum = 250
                    }
                ],
                Results = [
                    new() {
                        DisplayName = "Average True Range",
                        TooltipTemplate = "ATR([P1])",
                        DataName = "atr",
                        DataType = "number",
                        LineType = "solid",
                        DefaultColor = ChartColors.StandardBlue
                    }
                ]
            },

            // Average True Range Percent
            new IndicatorListing {
                Name = "Average True Range (ATR) Percent",
                Uiid = "ATRP",
                LegendTemplate = "ATR([P1]) %",
                Endpoint = $"{baseUrl}/ATR/",
                Category = "price-characteristic",
                ChartType = "oscillator",
                Parameters =
                [
                    new() {
                        DisplayName = "Lookback Periods",
                        ParamName = "lookbackPeriods",
                        DataType = "int",
                        DefaultValue = 14,
                        Minimum = 2,
                        Maximum = 250
                    }
                ],
                Results = [
                    new() {
                        DisplayName = "Average True Range Percent",
                        TooltipTemplate = "ATR([P1]) %",
                        DataName = "atrp",
                        DataType = "number",
                        LineType = "solid",
                        DefaultColor = ChartColors.StandardBlue
                    }
                ]
            },

            // Beta
            new IndicatorListing {
                Name = "Beta",
                Uiid = "BETA",
                LegendTemplate = "BETA([P1])",
                Endpoint = $"{baseUrl}/BETA/",
                Category = "price-characteristic",
                ChartType = "oscillator",
                ChartConfig = new ChartConfig {
                    Thresholds =
                    [
                        new() {
                            Value = 1,
                            Color = ChartColors.ThresholdGrayTransparent,
                            Style = "dash"
                        }
                    ]
                },
                Parameters =
                [
                    new() {
                        DisplayName = "Lookback Periods",
                        ParamName = "lookbackPeriods",
                        DataType = "int",
                        DefaultValue = 50,
                        Minimum = 1,
                        Maximum = 250
                    }
                ],
                Results = [
                    new() {
                        DisplayName = "Beta",
                        TooltipTemplate = "Beta",
                        DataName = "beta",
                        DataType = "number",
                        LineType = "solid",
                        DefaultColor = ChartColors.StandardBlue
                    },
                    new() {
                        DisplayName = "Beta+",
                        TooltipTemplate = "Beta+",
                        DataName = "betaUp",
                        DataType = "number",
                        LineType = "dash",
                        DefaultColor = ChartColors.StandardGreen
                    },
                    new() {
                        DisplayName = "Beta-",
                        TooltipTemplate = "Beta-",
                        DataName = "betaDown",
                        DataType = "number",
                        LineType = "dash",
                        DefaultColor = ChartColors.StandardRed
                    }
                ]
            },

            // Bollinger Bands
            new IndicatorListing {
                Name = "Bollinger Bands®",
                Uiid = "BB",
                LegendTemplate = "BB([P1],[P2])",
                Endpoint = $"{baseUrl}/BB/",
                Category = "price-channel",
                ChartType = "overlay",
                Order = Order.BehindPrice,
                Parameters =
                [
                    new() {
                        DisplayName = "Lookback Periods",
                        ParamName = "lookbackPeriods",
                        DataType = "int",
                        DefaultValue = 20,
                        Minimum = 2,
                        Maximum = 250
                    },
                    new() {
                        DisplayName = "Standard Deviations",
                        ParamName = "standardDeviations",
                        DataType = "number",
                        DefaultValue = 2,
                        Minimum = 0.01,
                        Maximum = 10
                    }
                ],
                Results = [
                    new() {
                        DisplayName = "Upper Band",
                        TooltipTemplate = "BB([P1],[P2]) Upper Band",
                        DataName = "upperBand",
                        DataType = "number",
                        LineType = "solid",
                        LineWidth = 1,
                        DefaultColor = ChartColors.DarkGray,
                        Fill = new ChartFill {
                            Target = "+2",
                            ColorAbove = ChartColors.DarkGrayTransparent,
                            ColorBelow = ChartColors.DarkGrayTransparent
                        }
                    },
                    new() {
                        DisplayName = "Centerline",
                        TooltipTemplate = "BB([P1],[P2]) Centerline",
                        DataName = "sma",
                        DataType = "number",
                        LineType = "dash",
                        LineWidth = 1,
                        DefaultColor = ChartColors.DarkGray
                    },
                    new() {
                        DisplayName = "Lower Band",
                        TooltipTemplate = "BB([P1],[P2]) Lower Band",
                        DataName = "lowerBand",
                        DataType = "number",
                        LineType = "solid",
                        LineWidth = 1,
                        DefaultColor = ChartColors.DarkGray
                    }
                ]
            },

            // Bollinger Bands %B
            new IndicatorListing {
                Name = "Bollinger Bands® %B",
                Uiid = "BB-PCTB",
                LegendTemplate = "BB([P1],[P2]) %B",
                Endpoint = $"{baseUrl}/BB/",
                Category = "oscillator",
                ChartType = "oscillator",
                ChartConfig = new ChartConfig {
                    Thresholds =
                    [
                        new() {
                            Value = 1,
                            Color = ChartColors.ThresholdRed,
                            Style = "dash",
                            Fill = new ChartFill {
                                Target = "+2",
                                ColorAbove = "transparent",
                                ColorBelow = ChartColors.ThresholdGreen
                            }
                        },
                        new() {
                            Value = 0,
                            Color = ChartColors.ThresholdGreen,
                            Style = "dash",
                            Fill = new ChartFill {
                                Target = "+1",
                                ColorAbove = ChartColors.ThresholdRed,
                                ColorBelow = "transparent"
                            }
                        }
                    ]
                },
                Parameters =
                [
                    new() {
                        DisplayName = "Lookback Periods",
                        ParamName = "lookbackPeriods",
                        DataType = "int",
                        DefaultValue = 20,
                        Minimum = 2,
                        Maximum = 250
                    },
                    new() {
                        DisplayName = "Standard Deviations",
                        ParamName = "standardDeviations",
                        DataType = "number",
                        DefaultValue = 2,
                        Minimum = 0.01,
                        Maximum = 10
                    }
                ],
                Results = [
                    new() {
                        DisplayName = "%B",
                        TooltipTemplate = "BB([P1],[P2]) %B",
                        DataName = "percentB",
                        DataType = "number",
                        LineType = "solid",
                        DefaultColor = ChartColors.StandardBlue,
                    }
                ]
            },

            // Chaikin Money Flow
            new IndicatorListing {
                Name = "Chaikin Money Flow (CMF)",
                Uiid = "CMF",
                LegendTemplate = "CMF([P1])",
                Endpoint = $"{baseUrl}/CMF/",
                Category = "volume-based",
                ChartType = "oscillator",
                ChartConfig = new ChartConfig {
                    Thresholds =
                    [
                        new() {
                            Value = 0,
                            Color = ChartColors.ThresholdGrayTransparent,
                            Style = "dash"
                        }
                    ]
                },
                Parameters =
                [
                    new() {
                        DisplayName = "Lookback Periods",
                        ParamName = "lookbackPeriods",
                        DataType = "int",
                        DefaultValue = 20,
                        Minimum = 1,
                        Maximum = 250
                    }
                ],
                Results = [
                    new() {
                        DisplayName = "CMF",
                        TooltipTemplate = "Chaikin Money Flow",
                        DataName = "cmf",
                        DataType = "number",
                        LineType = "solid",
                        DefaultColor = ChartColors.StandardBlue
                    }
                ]
            },

            // Chande Momentum Oscillator
            new IndicatorListing {
                Name = "Chande Momentum Oscillator",
                Uiid = "CMO",
                LegendTemplate = "CMO([P1])",
                Endpoint = $"{baseUrl}/CMO/",
                Category = "oscillator",
                ChartType = "oscillator",
                Parameters =
                [
                    new() {
                        DisplayName = "Lookback Periods",
                        ParamName = "lookbackPeriods",
                        DataType = "int",
                        DefaultValue = 14,
                        Minimum = 1,
                        Maximum = 250
                    }
                ],
                Results = [
                    new() {
                        DisplayName = "Chande Momentum Oscillator",
                        TooltipTemplate = "CMO([P1])",
                        DataName = "cmo",
                        DataType = "number",
                        LineType = "solid",
                        DefaultColor = ChartColors.StandardBlue
                    }
                ]
            },

            // Chandelier Exit (long)
            new IndicatorListing {
                Name = "Chandelier Exit (long)",
                Uiid = "CHEXIT-LONG",
                LegendTemplate = "CHANDELIER([P1],[P2],LONG)",
                Endpoint = $"{baseUrl}/CHEXIT-LONG/",
                Category = "stop-and-reverse",
                ChartType = "overlay",
                Parameters =
                [
                    new() {
                        DisplayName = "Lookback Periods",
                        ParamName = "lookbackPeriods",
                        DataType = "int",
                        DefaultValue = 22,
                        Minimum = 1,
                        Maximum = 250
                    },
                    new() {
                        DisplayName = "Multiplier",
                        ParamName = "multiplier",
                        DataType = "number",
                        DefaultValue = 3,
                        Minimum = 1,
                        Maximum = 10
                    }
                ],
                Results = [
                    new() {
                        DisplayName = "Chandelier Exit",
                        TooltipTemplate = "CHANDELIER([P1],[P2],LONG)",
                        DataName = "chandelierExit",
                        DataType = "number",
                        LineType = "solid",
                        DefaultColor = ChartColors.StandardOrange
                    }
                ]
            },

            // Chandelier Exit (short)
            new IndicatorListing {
                Name = "Chandelier Exit (short)",
                Uiid = "CHEXIT-SHORT",
                LegendTemplate = "CHANDELIER([P1],[P2],SHORT)",
                Endpoint = $"{baseUrl}/CHEXIT-SHORT/",
                Category = "stop-and-reverse",
                ChartType = "overlay",
                Parameters =
                [
                    new() {
                        DisplayName = "Lookback Periods",
                        ParamName = "lookbackPeriods",
                        DataType = "int",
                        DefaultValue = 22,
                        Minimum = 1,
                        Maximum = 250
                    },
                    new() {
                        DisplayName = "Multiplier",
                        ParamName = "multiplier",
                        DataType = "number",
                        DefaultValue = 3,
                        Minimum = 1,
                        Maximum = 10
                    }
                ],
                Results = [
                    new() {
                        DisplayName = "Chandelier Exit",
                        TooltipTemplate = "CHANDELIER([P1],[P2],SHORT)",
                        DataName = "chandelierExit",
                        DataType = "number",
                        LineType = "dash",
                        DefaultColor = ChartColors.StandardOrange
                    }
                ]
            },

            // Choppiness Index
            new IndicatorListing {
                Name = "Choppiness Index",
                Uiid = "CHOP",
                LegendTemplate = "CHOP([P1])",
                Endpoint = $"{baseUrl}/CHOP/",
                Category = "oscillator",
                ChartType = "oscillator",
                ChartConfig = new ChartConfig {
                    //MinimumYAxis = 0,
                    //MaximumYAxis = 100,

                    Thresholds =
                    [
                        new() {
                            Value = 61.8,
                            Color = ChartColors.DarkGrayTransparent,
                            Style = "dash",
                            Fill = new ChartFill {
                                Target = "+2",
                                ColorAbove = "transparent",
                                ColorBelow = ChartColors.ThresholdRed
                            }
                        },
                        new() {
                            Value = 38.2,
                            Color = ChartColors.DarkGrayTransparent,
                            Style = "dash",
                            Fill = new ChartFill {
                                Target = "+1",
                                ColorAbove = ChartColors.ThresholdGreen,
                                ColorBelow = "transparent"
                            }
                        }
                    ]
                },
                Parameters =
                [
                    new() {
                        DisplayName = "Lookback Periods",
                        ParamName = "lookbackPeriods",
                        DataType = "int",
                        DefaultValue = 14,
                        Minimum = 1,
                        Maximum = 250
                    }
                ],
                Results = [
                    new() {
                        DisplayName = "Choppiness",
                        TooltipTemplate = "Choppiness",
                        DataName = "chop",
                        DataType = "number",
                        LineType = "solid",
                        DefaultColor = ChartColors.StandardBlue
                    }
                ]
            },

            // ConnorsRSI
            new IndicatorListing {
                Name = "ConnorsRSI (CRSI)",
                Uiid = "CRSI",
                LegendTemplate = "CRSI([P1],[P2],[P3])",
                Endpoint = $"{baseUrl}/CRSI/",
                Category = "oscillator",
                ChartType = "oscillator",
                ChartConfig = new ChartConfig {
                    MinimumYAxis = 0,
                    MaximumYAxis = 100,

                    Thresholds =
                    [
                        new() {
                            Value = 90,
                            Color = ChartColors.ThresholdRed,
                            Style = "dash",
                            Fill = new ChartFill {
                                Target = "+2",
                                ColorAbove = "transparent",
                                ColorBelow = ChartColors.ThresholdGreen
                            }
                        },
                        new() {
                            Value = 10,
                            Color = ChartColors.ThresholdGreen,
                            Style = "dash",
                            Fill = new ChartFill {
                                Target = "+1",
                                ColorAbove = ChartColors.ThresholdRed,
                                ColorBelow = "transparent"
                            }
                        }
                    ]
                },
                Parameters =
                [
                    new() {
                        DisplayName = "Lookback Periods",
                        ParamName = "rsiPeriods",
                        DataType = "int",
                        DefaultValue = 3,
                        Minimum = 2,
                        Maximum = 250
                    },
                    new() {
                        DisplayName = "Streak Periods",
                        ParamName = "streakPeriods",
                        DataType = "int",
                        DefaultValue = 2,
                        Minimum = 2,
                        Maximum = 50
                    },
                    new() {
                        DisplayName = "Rank Periods",
                        ParamName = "rankPeriods",
                        DataType = "int",
                        DefaultValue = 100,
                        Minimum = 2,
                        Maximum = 250
                    }
                ],
                Results = [
                    new() {
                        DisplayName = "CRSI",
                        TooltipTemplate = "CRSI([P1],[P2],[P3])",
                        DataName = "connorsRsi",
                        DataType = "number",
                        LineType = "solid",
                        DefaultColor = ChartColors.StandardBlue
                    }
                ]
            },

            // Doji
            new IndicatorListing {
                Name = "Doji",
                Uiid = "DOJI",
                LegendTemplate = "DOJI([P1]%)",
                Endpoint = $"{baseUrl}/DOJI/",
                Category = "candlestick-pattern",
                ChartType = "overlay",
                Parameters =
                [
                    new() {
                        DisplayName = "Max Price Change %",
                        ParamName = "maxPriceChangePercent",
                        DataType = "number",
                        DefaultValue = 0.1,
                        Minimum = 0,
                        Maximum = 0.5
                    }
                ],
                Results = [
                    new() {
                        TooltipTemplate = "DOJI([P1]%)",
                        DisplayName = "Doji",
                        DataName = "price",
                        DataType = "number",
                        LineType = "pointer",
                        LineWidth = 8,
                        DefaultColor = ChartColors.DarkGray
                    }
                ]
            },

            // Dominant Cycle Period (HT Trendline)
            new IndicatorListing {
                Name = "Dominant Cycle Periods",
                Uiid = "DCPERIOD",
                LegendTemplate = "DC PERIODS",
                Endpoint = $"{baseUrl}/HTL/",
                Category = "price-characteristic",
                ChartType = "oscillator",
                ChartConfig = new ChartConfig {
                    MinimumYAxis = 0
                },
                Results = [
                    new() {
                        DisplayName = "DC PERIODS",
                        TooltipTemplate = "DC PERIODS",
                        DataName = "dcPeriods",
                        DataType = "number",
                        LineType = "bar",
                        DefaultColor = ChartColors.StandardBlue
                    }
                ]
            },

            // Donchian Channels
            new IndicatorListing {
                Name = "Donchian Channels",
                Uiid = "DONCHIAN",
                LegendTemplate = "DONCHIAN([P1])",
                Endpoint = $"{baseUrl}/DONCHIAN/",
                Category = "price-channel",
                ChartType = "overlay",
                Order = Order.BehindPrice,
                Parameters =
                [
                    new() {
                        DisplayName = "Lookback Periods",
                        ParamName = "lookbackPeriods",
                        DataType = "int",
                        DefaultValue = 20,
                        Minimum = 1,
                        Maximum = 250
                    }
                ],
                Results = MetadataHelpers.GetPriceBandResults("DONCHIAN", ChartColors.StandardOrange)
            },

            // Dynamic, McGinley
            new IndicatorListing {
                Name = "McGinley Dynamic",
                Uiid = "DYN",
                LegendTemplate = "DYNAMIC([P1])",
                Endpoint = $"{baseUrl}/DYN/",
                Category = "moving-average",
                ChartType = "overlay",
                Parameters =
                [
                    new() {
                        DisplayName = "Lookback Periods",
                        ParamName = "lookbackPeriods",
                        DataType = "int",
                        DefaultValue = 10,
                        Minimum = 1,
                        Maximum = 250
                    }
                ],
                Results = [
                    new() {
                        DisplayName = "Dynamic",
                        TooltipTemplate = "DYNAMIC([P1])",
                        DataName = "dynamic",
                        DataType = "number",
                        LineType = "solid",
                        DefaultColor = ChartColors.StandardBlue
                    }
                ]
            },

            // Elder-ray
            new IndicatorListing {
                Name = "Elder-ray Index",
                Uiid = "ELDER-RAY",
                LegendTemplate = "ELDER-RAY([P1])",
                Endpoint = $"{baseUrl}/ELDER-RAY/",
                Category = "price-trend",
                ChartType = "oscillator",
                Parameters =
                [
                    new() {
                        DisplayName = "Lookback Periods",
                        ParamName = "lookbackPeriods",
                        DataType = "int",
                        DefaultValue = 13,
                        Minimum = 1,
                        Maximum = 250
                    }
                ],
                Results = [
                    new() {
                        DisplayName = "Bull Power",
                        TooltipTemplate = "Bull Power",
                        DataName = "bullPower",
                        DataType = "number",
                        LineType = "bar",
                        Stack = "eray",
                        DefaultColor = ChartColors.StandardGreen
                    },
                    new() {
                        DisplayName = "Bear Power",
                        TooltipTemplate = "Bear Power",
                        DataName = "bearPower",
                        DataType = "number",
                        LineType = "bar",
                        Stack = "eray",
                        DefaultColor = ChartColors.StandardRed
                    }
                ]
            },

            // Endpoint Moving Average
            new IndicatorListing {
                Name = "Endpoint Moving Average (EPMA)",
                Uiid = "EPMA",
                LegendTemplate = "EPMA([P1])",
                Endpoint = $"{baseUrl}/EPMA/",
                Category = "moving-average",
                ChartType = "overlay",
                Parameters =
                [
                    new() {
                        DisplayName = "Lookback Periods",
                        ParamName = "lookbackPeriods",
                        DataType = "int",
                        DefaultValue = 10,
                        Minimum = 1,
                        Maximum = 250
                    }
                ],
                Results = [
                    new() {
                        DisplayName = "EPMA",
                        TooltipTemplate = "EPMA([P1])",
                        DataName = "epma",
                        DataType = "number",
                        LineType = "solid",
                        DefaultColor = ChartColors.StandardBlue
                    }
                ]
            },

            // Exponential Moving Average
            new IndicatorListing {
                Name = "Exponential Moving Average (EMA)",
                Uiid = "EMA",
                LegendTemplate = "EMA([P1])",
                Endpoint = $"{baseUrl}/EMA/",
                Category = "moving-average",
                ChartType = "overlay",
                Parameters =
                [
                    new() {
                        DisplayName = "Lookback Periods",
                        ParamName = "lookbackPeriods",
                        DataType = "int",
                        DefaultValue = 20,
                        Minimum = 1,
                        Maximum = 250
                    }
                ],
                Results = [
                    new() {
                        DisplayName = "EMA",
                        TooltipTemplate = "EMA([P1])",
                        DataName = "ema",
                        DataType = "number",
                        LineType = "solid",
                        DefaultColor = ChartColors.StandardBlue
                    }
                ]
            },

            // Fisher Transform
            new IndicatorListing {
                Name = "Ehlers Fisher Transform",
                Uiid = "FISHER",
                LegendTemplate = "FISHER([P1])",
                Endpoint = $"{baseUrl}/FISHER/",
                Category = "price-transform",
                ChartType = "oscillator",
                Parameters =
                [
                    new() {
                        DisplayName = "Lookback Periods",
                        ParamName = "lookbackPeriods",
                        DataType = "int",
                        DefaultValue = 10,
                        Minimum = 1,
                        Maximum = 250
                    }
                ],
                Results = [
                    new() {
                        DisplayName = "Fisher Transform",
                        TooltipTemplate = "Fisher Transform",
                        DataName = "fisher",
                        DataType = "number",
                        LineType = "solid",
                        DefaultColor = ChartColors.StandardBlue
                    },
                    new() {
                        DisplayName = "Trigger",
                        TooltipTemplate = "Trigger",
                        DataName = "trigger",
                        DataType = "number",
                        LineType = "solid",
                        DefaultColor = ChartColors.StandardRed
                    }
                ]
            },

            // Fractal (Williams)
            new IndicatorListing {
                Name = "Williams Fractal (high/low)",
                Uiid = "FRACTAL",
                LegendTemplate = "FRACTAL([P1])",
                Endpoint = $"{baseUrl}/FRACTAL/",
                Category = "price-pattern",
                ChartType = "overlay",
                Parameters =
                [
                    new() {
                        DisplayName = "Window Span",
                        ParamName = "windowSpan",
                        DataType = "int",
                        DefaultValue = 2,
                        Minimum = 1,
                        Maximum = 100
                    }
                ],
                Results = [
                    new() {
                        DisplayName = "Fractal Bull",
                        TooltipTemplate = "Fractal Bull ([P1])",
                        DataName = "fractalBull",
                        DataType = "number",
                        LineType = "dots",
                        LineWidth = 3,
                        DefaultColor = ChartColors.StandardRed
                    },
                    new() {
                        DisplayName = "Fractal Bear",
                        TooltipTemplate = "Fractal Bear ([P1])",
                        DataName = "fractalBear",
                        DataType = "number",
                        LineType = "dots",
                        LineWidth = 3,
                        DefaultColor = ChartColors.StandardGreen
                    }
                ]
            },

            // Fractal Chaos Bands (FCB)
            new IndicatorListing {
                Name = "Fractal Chaos Bands",
                Uiid = "FCB",
                LegendTemplate = "FCB([P1])",
                Endpoint = $"{baseUrl}/FCB/",
                Category = "price-channels",
                ChartType = "overlay",
                Order = Order.Front,
                Parameters =
                [
                    new() {
                        DisplayName = "Window Span",
                        ParamName = "windowSpan",
                        DataType = "int",
                        DefaultValue = 2,
                        Minimum = 2,
                        Maximum = 30
                    }
                ],
                Results = [
                    new() {
                        TooltipTemplate = "Upper Band",
                        DisplayName = "FCB([P1]) Upper Band",
                        DataName = "upperBand",
                        DataType = "number",
                        LineType = "solid",
                        DefaultColor = ChartColors.StandardGreen
                    },
                    new() {
                        DisplayName = "Lower Band",
                        TooltipTemplate = "FCB([P1]) Lower Band",
                        DataName = "lowerBand",
                        DataType = "number",
                        LineType = "solid",
                        DefaultColor = ChartColors.StandardRed
                    }
                ]
            },

            // Hilbert Transform Instantaneous Trendline
            new IndicatorListing {
                Name = "Hilbert Transform Instantaneous Trendline",
                Uiid = "HT Trendline",
                LegendTemplate = "HT TRENDLINE",
                Endpoint = $"{baseUrl}/HTL/",
                Category = "moving-average",
                ChartType = "overlay",
                Results = [
                    new() {
                        DisplayName = "HT Trendline",
                        TooltipTemplate = "HT Trendline",
                        DataName = "trendline",
                        DataType = "number",
                        LineType = "solid",
                        DefaultColor = ChartColors.StandardBlue
                    },
                    new() {
                        DisplayName = "HT Smoothed Price",
                        TooltipTemplate = "HT Smooth Price",
                        DataName = "smoothPrice",
                        DataType = "number",
                        LineType = "solid",
                        DefaultColor = ChartColors.StandardOrange
                    }
                ]
            },

            // Ichimoku Cloud
            new IndicatorListing {
                Name = "Ichimoku Cloud",
                Uiid = "ICHIMOKU",
                LegendTemplate = "ICHIMOKU([P1],[P2],[P3])",
                Endpoint = $"{baseUrl}/ICHIMOKU/",
                Category = "price-trend",
                ChartType = "overlay",
                Order = Order.BehindPrice,
                Parameters =
                [
                    new() {
                        DisplayName = "Tenkan Periods",
                        ParamName = "tenkanPeriods",
                        DataType = "int",
                        DefaultValue = 9,
                        Minimum = 1,
                        Maximum = 250
                    },
                    new() {
                        DisplayName = "Kijun Periods",
                        ParamName = "kijunPeriods",
                        DataType = "int",
                        DefaultValue = 26,
                        Minimum = 2,
                        Maximum = 250
                    },
                    new() {
                        DisplayName = "Senkou Periods",
                        ParamName = "senkouBPeriods",
                        DataType = "int",
                        DefaultValue = 52,
                        Minimum = 3,
                        Maximum = 250
                    }
                ],
                Results = [
                    new() {
                        DisplayName = "Tenkan-sen",
                        TooltipTemplate = "ICHIMOKU([P1],[P2],[P3] Tenkan-sen",
                        DataName = "tenkanSen",
                        DataType = "number",
                        LineType = "solid",
                        LineWidth = 2,
                        DefaultColor = ChartColors.StandardBlue,
                    },
                    new() {
                        DisplayName = "Kijun-sen",
                        TooltipTemplate = "ICHIMOKU([P1],[P2],[P3] Kijun-sen",
                        DataName = "kijunSen",
                        DataType = "number",
                        LineType = "solid",
                        LineWidth = 2,
                        DefaultColor = ChartColors.StandardPurple,
                    },
                    new() {
                        DisplayName = "Chikou span",
                        TooltipTemplate = "ICHIMOKU([P1],[P2],[P3] Chikou span",
                        DataName = "chikouSpan",
                        DataType = "number",
                        LineType = "solid",
                        LineWidth = 2,
                        DefaultColor = ChartColors.DarkGray,
                    },
                    new() {
                        DisplayName = "Senkou span A",
                        TooltipTemplate = "ICHIMOKU([P1],[P2],[P3] Senkou span A",
                        DataName = "senkouSpanA",
                        DataType = "number",
                        LineType = "solid",
                        LineWidth = 1.5f,
                        DefaultColor = ChartColors.ThresholdGreen,
                    },
                    new() {
                        DisplayName = "Senkou span B",
                        TooltipTemplate = "ICHIMOKU([P1],[P2],[P3] Senkou span B",
                        DataName = "senkouSpanB",
                        DataType = "number",
                        LineType = "solid",
                        LineWidth = 1.5f,
                        DefaultColor = ChartColors.ThresholdRed,
                        Fill = new ChartFill {
                            Target = "-1",
                            ColorAbove = ChartColors.ThresholdRedTransparent,
                            ColorBelow = ChartColors.ThresholdGreenTransparent
                        }
                    }
                ]
            },


            // Keltner Channels
            new IndicatorListing {
                Name = "Keltner Channels",
                Uiid = "KELTNER",
                LegendTemplate = "KELTNER([P1],[P2],[P3])",
                Endpoint = $"{baseUrl}/KELTNER/",
                Category = "price-channel",
                ChartType = "overlay",
                Order = Order.BehindPrice,
                Parameters =
                [
                    new() {
                        DisplayName = "EMA Periods",
                        ParamName = "emaPeriods",
                        DataType = "int",
                        DefaultValue = 20,
                        Minimum = 2,
                        Maximum = 250
                    },
                    new() {
                        DisplayName = "Multiplier",
                        ParamName = "multiplier",
                        DataType = "number",
                        DefaultValue = 2,
                        Minimum = 0.01,
                        Maximum = 10
                    },
                    new() {
                        DisplayName = "ATR Periods",
                        ParamName = "atrPeriods",
                        DataType = "number",
                        DefaultValue = 10,
                        Minimum = 2,
                        Maximum = 250
                    }
                ],
                Results = MetadataHelpers.GetPriceBandResults("KELTNER", ChartColors.StandardOrange)
            },

            // Marubozu
            new IndicatorListing {
                Name = "Marubozu",
                Uiid = "MARUBOZU",
                LegendTemplate = "MARUBOZU([P1]%)",
                Endpoint = $"{baseUrl}/MARUBOZU/",
                Category = "candlestick-pattern",
                ChartType = "overlay",
                Parameters =
                [
                    new() {
                        DisplayName = "Min Body Percent %",
                        ParamName = "minBodyPercent",
                        DataType = "number",
                        DefaultValue = 90,
                        Minimum = 80,
                        Maximum = 100
                    }
                ],
                Results = [
                    new() {
                        DisplayName = "Marubozu",
                        TooltipTemplate = "MARUBOZU([P1]%)",
                        DataName = "price",
                        DataType = "number",
                        LineType = "pointer",
                        LineWidth = 8,
                        DefaultColor = ChartColors.DarkGray
                    }
                ]
            },

            // Money Flow Index
            new IndicatorListing {
                Name = "Money Flow Index (MFI)",
                Uiid = "MFI",
                LegendTemplate = "MFI([P1])",
                Endpoint = $"{baseUrl}/MFI/",
                Category = "volume-based",
                ChartType = "oscillator",
                ChartConfig = new ChartConfig {
                    MinimumYAxis = 0,
                    MaximumYAxis = 100,

                    Thresholds =
                    [
                        new() {
                            Value = 80,
                            Color = ChartColors.ThresholdRed,
                            Style = "dash",
                            Fill = new ChartFill {
                                Target = "+2",
                                ColorAbove = "transparent",
                                ColorBelow = ChartColors.ThresholdGreen
                            }
                        },
                        new() {
                            Value = 20,
                            Color = ChartColors.ThresholdGreen,
                            Style = "dash",
                            Fill = new ChartFill {
                                Target = "+1",
                                ColorAbove = ChartColors.ThresholdRed,
                                ColorBelow = "transparent"
                            }
                        }
                    ]
                },
                Parameters =
                [
                    new() {
                        DisplayName = "Lookback Periods",
                        ParamName = "lookbackPeriods",
                        DataType = "int",
                        DefaultValue = 14,
                        Minimum = 1,
                        Maximum = 250
                    }
                ],
                Results = [
                    new() {
                        DisplayName = "MFI",
                        TooltipTemplate = "MFI([P1])",
                        DataName = "mfi",
                        DataType = "number",
                        LineType = "solid",
                        DefaultColor = ChartColors.StandardBlue
                    }
                ]
            },

            // Moving Average Convergence/Divergence
            new IndicatorListing {
                Name = "Moving Average Convergence/Divergence (MACD)",
                Uiid = "MACD",
                LegendTemplate = "MACD([P1],[P2],[P3])",
                Endpoint = $"{baseUrl}/MACD/",
                Category = "price-trend",
                ChartType = "oscillator",
                ChartConfig = new ChartConfig {
                    Thresholds =
                    [
                        new() {
                            Value = 0,
                            Color = ChartColors.DarkGrayTransparent,
                            Style = "dash"
                        }
                    ]
                },
                Parameters =
                [
                    new() {
                        DisplayName = "Fast Periods",
                        ParamName = "fastPeriods",
                        DataType = "int",
                        DefaultValue = 12,
                        Minimum = 1,
                        Maximum = 200
                    },
                    new() {
                        DisplayName = "Slow Periods",
                        ParamName = "slowPeriods",
                        DataType = "int",
                        DefaultValue = 26,
                        Minimum = 1,
                        Maximum = 250
                    },
                    new() {
                        DisplayName = "Signal Periods",
                        ParamName = "signalPeriods",
                        DataType = "int",
                        DefaultValue = 9,
                        Minimum = 1,
                        Maximum = 50
                    }
                ],
                Results = [
                    new() {
                        DisplayName = "MACD",
                        TooltipTemplate = "MACD",
                        DataName = "macd",
                        DataType = "number",
                        LineType = "solid",
                        DefaultColor = ChartColors.StandardBlue
                    },
                    new() {
                        DisplayName = "Signal",
                        TooltipTemplate = "Signal",
                        DataName = "signal",
                        DataType = "number",
                        LineType = "solid",
                        DefaultColor = ChartColors.StandardRed
                    },
                    new() {
                        DisplayName = "Histogram",
                        TooltipTemplate = "Histogram",
                        DataName = "histogram",
                        DataType = "number",
                        LineType = "bar",
                        DefaultColor = ChartColors.StandardGrayTransparent
                    }
                ]
            },

            // Parabolic SAR
            new IndicatorListing {
                Name = "Parabolic Stop and Reverse (SAR)",
                Uiid = "PSAR",
                LegendTemplate = "PSAR([P1],[P2])",
                Endpoint = $"{baseUrl}/PSAR/",
                Category = "stop-and-reverse",
                ChartType = "overlay",

                Parameters =
                [
                    new() {
                        DisplayName = "Step Size",
                        ParamName = "accelerationStep",
                        DataType = "number",
                        DefaultValue = 0.02,
                        Minimum = 0.000001,
                        Maximum = 2500
                    },
                    new() {
                        DisplayName = "Max Factor",
                        ParamName = "maxAccelerationFactor",
                        DataType = "number",
                        DefaultValue = 0.2,
                        Minimum = 0.000001,
                        Maximum = 2500
                    }
                ],
                Results = [
                    new() {
                        DisplayName = "Parabolic SAR",
                        TooltipTemplate = "PSAR([P1],[P2])",
                        DataName = "sar",
                        DataType = "number",
                        LineType = "dots",
                        LineWidth = 2,
                        DefaultColor = ChartColors.StandardPurple
                    }
                ]
            },

            // Rate of Change
            new IndicatorListing {
                Name = "Rate of Change",
                Uiid = "ROC",
                LegendTemplate = "ROC([P1],[P2])",
                Endpoint = $"{baseUrl}/ROC/",
                Category = "oscillator",
                ChartType = "oscillator",
                Parameters =
                [
                    new() {
                        DisplayName = "Lookback Periods",
                        ParamName = "lookbackPeriods",
                        DataType = "int",
                        DefaultValue = 14,
                        Minimum = 1,
                        Maximum = 250
                    },
                    new() {
                        DisplayName = "SMA Periods",
                        ParamName = "smaPeriods",
                        DataType = "int",
                        DefaultValue = 3,
                        Minimum = 1,
                        Maximum = 50
                    }
                ],
                Results = [
                    new() {
                        DisplayName = "Rate of Change",
                        TooltipTemplate = "ROC([P1],[P2])",
                        DataName = "roc",
                        DataType = "number",
                        LineType = "solid",
                        DefaultColor = ChartColors.StandardBlue
                    },
                    new() {
                        DisplayName = "SMA of ROC",
                        TooltipTemplate = "STO %D([P2])",
                        DataName = "rocSma",
                        DataType = "number",
                        LineType = "solid",
                        DefaultColor = ChartColors.StandardRed
                    }
                ]
            },

            // Relative Strength Index
            new IndicatorListing {
                Name = "Relative Strength Index (RSI)",
                Uiid = "RSI",
                LegendTemplate = "RSI([P1])",
                Endpoint = $"{baseUrl}/RSI/",
                Category = "oscillator",
                ChartType = "oscillator",
                ChartConfig = new ChartConfig {
                    MinimumYAxis = 0,
                    MaximumYAxis = 100,

                    Thresholds =
                    [
                        new() {
                            Value = 70,
                            Color = ChartColors.ThresholdRed,
                            Style = "dash",
                            Fill = new ChartFill {
                                Target = "+2",
                                ColorAbove = "transparent",
                                ColorBelow = ChartColors.ThresholdGreen
                            }
                        },
                        new() {
                            Value = 30,
                            Color = ChartColors.ThresholdGreen,
                            Style = "dash",
                            Fill = new ChartFill {
                                Target = "+1",
                                ColorAbove = ChartColors.ThresholdRed,
                                ColorBelow = "transparent"
                            }
                        }
                    ]
                },
                Parameters =
                [
                    new() {
                        DisplayName = "Lookback Periods",
                        ParamName = "lookbackPeriods",
                        DataType = "int",
                        DefaultValue = 14,
                        Minimum = 1,
                        Maximum = 250
                    }
                ],
                Results = [
                    new() {
                        DisplayName = "RSI",
                        TooltipTemplate = "RSI([P1])",
                        DataName = "rsi",
                        DataType = "number",
                        LineType = "solid",
                        DefaultColor = ChartColors.StandardBlue
                    }
                ]
            },

            // Schaff Trend Cycle (STC)
            new IndicatorListing {
                Name = "Schaff Trend Cycle (STC)",
                Uiid = "STC",
                LegendTemplate = "STC([P1],[P2],[P3])",
                Endpoint = $"{baseUrl}/STC/",
                Category = "oscillator",
                ChartType = "oscillator",
                ChartConfig = new ChartConfig {
                    MinimumYAxis = 0,
                    MaximumYAxis = 100,

                    Thresholds =
                    [
                        new() {
                            Value = 75,
                            Color = ChartColors.ThresholdGreen,
                            Style = "solid",
                            Fill = new ChartFill {
                                Target = "+2",
                                ColorAbove = "transparent",
                                ColorBelow = ChartColors.ThresholdGreen
                            }
                        },
                        new() {
                            Value = 25,
                            Color = ChartColors.ThresholdRed,
                            Style = "solid",
                            Fill = new ChartFill {
                                Target = "+1",
                                ColorAbove = ChartColors.ThresholdRed,
                                ColorBelow = "transparent"
                            }
                        }
                    ]
                },
                Parameters =
                [
                    new() {
                        DisplayName = "Cycle Periods",
                        ParamName = "cyclePeriods",
                        DataType = "int",
                        DefaultValue = 10,
                        Minimum = 1,
                        Maximum = 250
                    },
                    new() {
                        DisplayName = "Fast Periods",
                        ParamName = "fastPeriods",
                        DataType = "int",
                        DefaultValue = 23,
                        Minimum = 1,
                        Maximum = 250
                    },
                    new() {
                        DisplayName = "Slow Periods",
                        ParamName = "slowPeriods",
                        DataType = "int",
                        DefaultValue = 50,
                        Minimum = 1,
                        Maximum = 250
                    }
                ],
                Results = [
                    new() {
                        DisplayName = "Schaff Trend Cycle",
                        TooltipTemplate = "Schaff Trend Cycle",
                        DataName = "stc",
                        DataType = "number",
                        LineType = "solid",
                        DefaultColor = ChartColors.StandardBlue
                    }
                ]
            },

            // Slope
            new IndicatorListing {
                Name = "Slope",
                Uiid = "SLOPE",
                LegendTemplate = "SLOPE([P1])",
                Endpoint = $"{baseUrl}/SLOPE/",
                Category = "price-characteristic",
                ChartType = "oscillator",
                Parameters =
                [
                    new() {
                        DisplayName = "Lookback Periods",
                        ParamName = "lookbackPeriods",
                        DataType = "int",
                        DefaultValue = 14,
                        Minimum = 2,
                        Maximum = 250
                    }
                ],
                Results = [
                    new() {
                        DisplayName = "Slope",
                        TooltipTemplate = "SLOPE([P1])",
                        DataName = "slope",
                        DataType = "number",
                        LineType = "solid",
                        DefaultColor = ChartColors.StandardBlue
                    }
                ]
            },

            // Linear Regression
            new IndicatorListing {
                Name = "Linear Regression",
                Uiid = "LINEAR",
                LegendTemplate = "LINEAR([P1])",
                Endpoint = $"{baseUrl}/SLOPE/",
                Category = "price-characteristic",
                ChartType = "overlay",
                Parameters =
                [
                    new() {
                        DisplayName = "Lookback Periods",
                        ParamName = "lookbackPeriods",
                        DataType = "int",
                        DefaultValue = 14,
                        Minimum = 1,
                        Maximum = 250
                    }
                ],
                Results = [
                    new() {
                        DisplayName = "Linear Regression",
                        TooltipTemplate = "LINEAR([P1])",
                        DataName = "line",
                        DataType = "number",
                        LineType = "solid",
                        DefaultColor = ChartColors.StandardBlue
                    }
                ]
            },

            // Simple Moving Average
            new IndicatorListing {
                Name = "Simple Moving Average (SMA)",
                Uiid = "SMA",
                LegendTemplate = "SMA([P1])",
                Endpoint = $"{baseUrl}/SMA/",
                Category = "moving-average",
                ChartType = "overlay",
                Parameters =
                [
                    new() {
                        DisplayName = "Lookback Periods",
                        ParamName = "lookbackPeriods",
                        DataType = "int",
                        DefaultValue = 10,
                        Minimum = 1,
                        Maximum = 250
                    }
                ],
                Results = [
                    new() {
                        DisplayName = "SMA",
                        TooltipTemplate = "SMA([P1])",
                        DataName = "sma",
                        DataType = "number",
                        LineType = "solid",
                        DefaultColor = ChartColors.StandardBlue
                    }
                ]
            },

            // Standard Deviation (absolute)
            new IndicatorListing {
                Name = "Standard Deviation (absolute)",
                Uiid = "STDEV",
                LegendTemplate = "STDEV([P1])",
                Endpoint = $"{baseUrl}/STDEV/",
                Category = "price-characteristic",
                ChartType = "oscillator",
                Parameters =
                [
                    new() {
                        DisplayName = "Lookback Periods",
                        ParamName = "lookbackPeriods",
                        DataType = "int",
                        DefaultValue = 14,
                        Minimum = 1,
                        Maximum = 250
                    },
                    new() {
                        DisplayName = "SMA Periods",
                        ParamName = "smaPeriods",
                        DataType = "int",
                        DefaultValue = 3,
                        Minimum = 1,
                        Maximum = 50
                    }
                ],
                Results = [
                    new() {
                        DisplayName = "Standard Deviation",
                        TooltipTemplate = "STDEV([P1])",
                        DataName = "stdDev",
                        DataType = "number",
                        LineType = "solid",
                        DefaultColor = ChartColors.StandardBlue
                    },
                    new() {
                        DisplayName = "SMA of Standard Deviation",
                        TooltipTemplate = "STDEV([P1]) SMA",
                        DataName = "stdDevSma",
                        DataType = "number",
                        LineType = "solid",
                        DefaultColor = ChartColors.StandardRed
                    }
                ]
            },

            // Standard Deviation (z-score)
            new IndicatorListing {
                Name = "Standard Deviation (Z-Score)",
                Uiid = "STDEV-ZSCORE",
                LegendTemplate = "STDEV-ZSCORE([P1])",
                Endpoint = $"{baseUrl}/STDEV/",
                Category = "price-characteristic",
                ChartType = "oscillator",
                Parameters =
                [
                    new() {
                        DisplayName = "Lookback Periods",
                        ParamName = "lookbackPeriods",
                        DataType = "int",
                        DefaultValue = 14,
                        Minimum = 1,
                        Maximum = 250
                    }
                ],
                Results = [
                    new() {
                        DisplayName = "Z-Score",
                        TooltipTemplate = "Z-Score([P1])",
                        DataName = "zScore",
                        DataType = "number",
                        LineType = "solid",
                        DefaultColor = ChartColors.StandardBlue
                    }
                ]
            },

            // STARC Bands
            new IndicatorListing {
                Name = "STARC Bands",
                Uiid = "STARC",
                LegendTemplate = "STARC([P1],[P2],[P3])",
                Endpoint = $"{baseUrl}/STARC/",
                Category = "price-channel",
                ChartType = "overlay",
                Order = Order.BehindPrice,
                Parameters =
                [
                    new() {
                        DisplayName = "SMA Periods",
                        ParamName = "smaPeriods",
                        DataType = "int",
                        DefaultValue = 5,
                        Minimum = 1,
                        Maximum = 50
                    },
                    new() {
                        DisplayName = "Multiplier",
                        ParamName = "multiplier",
                        DataType = "number",
                        DefaultValue = 2,
                        Minimum = 1,
                        Maximum = 10
                    },
                    new() {
                        DisplayName = "ATR Periods",
                        ParamName = "atrPeriods",
                        DataType = "int",
                        DefaultValue = 10,
                        Minimum = 1,
                        Maximum = 50
                    }
                ],
                Results = MetadataHelpers.GetPriceBandResults("STARC", ChartColors.StandardOrange)
            },

            // Stochastic Momentum Index
            new IndicatorListing {
                Name = "Stochastic Momentum Index",
                Uiid = "SMI",
                LegendTemplate = "SMI([P1],[P2],[P3],[P4])",
                Endpoint = $"{baseUrl}/SMI/",
                Category = "oscillator",
                ChartType = "oscillator",
                ChartConfig = new ChartConfig {
                    Thresholds =
                    [
                        new() {
                            Value = 40,
                            Color = ChartColors.ThresholdRed,
                            Style = "dash",
                            Fill = new ChartFill {
                                Target = "+2",
                                ColorAbove = "transparent",
                                ColorBelow = ChartColors.ThresholdGreen
                            }
                        },
                        new() {
                            Value = -40,
                            Color = ChartColors.ThresholdGreen,
                            Style = "dash",
                            Fill = new ChartFill {
                                Target = "+1",
                                ColorAbove = ChartColors.ThresholdRed,
                                ColorBelow = "transparent"
                            }
                        }
                    ]
                },
                Parameters =
                [
                    new() {
                        DisplayName = "Lookback Periods",
                        ParamName = "lookbackPeriods",
                        DataType = "int",
                        DefaultValue = 13,
                        Minimum = 1,
                        Maximum = 300
                    },
                    new() {
                        DisplayName = "First Smooth Periods",
                        ParamName = "firstSmoothPeriods",
                        DataType = "int",
                        DefaultValue = 25,
                        Minimum = 1,
                        Maximum = 300
                    },
                    new() {
                        DisplayName = "Second Smooth Periods",
                        ParamName = "secondSmoothPeriods",
                        DataType = "int",
                        DefaultValue = 2,
                        Minimum = 1,
                        Maximum = 50
                    },
                    new() {
                        DisplayName = "Signal Periods",
                        ParamName = "signalPeriods",
                        DataType = "int",
                        DefaultValue = 9,
                        Minimum = 1,
                        Maximum = 50
                    }
                ],
                Results = [
                    new() {
                        DisplayName = "SMI",
                        TooltipTemplate = "SMI",
                        DataName = "smi",
                        DataType = "number",
                        LineType = "solid",
                        DefaultColor = ChartColors.StandardBlue
                    },
                    new() {
                        DisplayName = "Signal",
                        TooltipTemplate = "Signal",
                        DataName = "signal",
                        DataType = "number",
                        LineType = "solid",
                        DefaultColor = ChartColors.StandardRed
                    }
                ]
            },

            // Stochastic Oscillator
            new IndicatorListing {
                Name = "Stochastic Oscillator",
                Uiid = "STO",
                LegendTemplate = "STOCH %K([P1]) %D([P2])",
                Endpoint = $"{baseUrl}/STO/",
                Category = "oscillator",
                ChartType = "oscillator",
                ChartConfig = new ChartConfig {
                    Thresholds =
                    [
                        new() {
                            Value = 80,
                            Color = ChartColors.ThresholdRed,
                            Style = "dash",
                            Fill = new ChartFill {
                                Target = "+2",
                                ColorAbove = "transparent",
                                ColorBelow = ChartColors.ThresholdGreen
                            }
                        },
                        new() {
                            Value = 20,
                            Color = ChartColors.ThresholdGreen,
                            Style = "dash",
                            Fill = new ChartFill {
                                Target = "+1",
                                ColorAbove = ChartColors.ThresholdRed,
                                ColorBelow = "transparent"
                            }
                        }
                    ]
                },
                Parameters =
                [
                    new() {
                        DisplayName = "Lookback Periods (%K)",
                        ParamName = "lookbackPeriods",
                        DataType = "int",
                        DefaultValue = 14,
                        Minimum = 1,
                        Maximum = 250
                    },
                    new() {
                        DisplayName = "Signal Periods (%D)",
                        ParamName = "signalPeriods",
                        DataType = "int",
                        DefaultValue = 3,
                        Minimum = 1,
                        Maximum = 250
                    }
                ],
                Results = [
                    new() {
                        DisplayName = "%K",
                        TooltipTemplate = "STO %K([P1])",
                        DataName = "k",
                        DataType = "number",
                        LineType = "solid",
                        DefaultColor = ChartColors.StandardBlue
                    },
                    new() {
                        DisplayName = "%D",
                        TooltipTemplate = "STO %D([P2])",
                        DataName = "d",
                        DataType = "number",
                        LineType = "solid",
                        DefaultColor = ChartColors.StandardRed
                    }
                ]
            },

            // Stochastic RSI
            new IndicatorListing {
                Name = "Stochastic RSI",
                Uiid = "STOCHRSI",
                LegendTemplate = "STOCH-RSI ([P1],[P2],[P3],[P4])",
                Endpoint = $"{baseUrl}/STORSI/",
                Category = "oscillator",
                ChartType = "oscillator",
                ChartConfig = new ChartConfig {
                    Thresholds =
                    [
                        new() {
                            Value = 80,
                            Color = ChartColors.ThresholdRed,
                            Style = "dash",
                            Fill = new ChartFill {
                                Target = "+2",
                                ColorAbove = "transparent",
                                ColorBelow = ChartColors.ThresholdGreen
                            }
                        },
                        new() {
                            Value = 20,
                            Color = ChartColors.ThresholdGreen,
                            Style = "dash",
                            Fill = new ChartFill {
                                Target = "+1",
                                ColorAbove = ChartColors.ThresholdRed,
                                ColorBelow = "transparent"
                            }
                        }
                    ]
                },
                Parameters =
                [
                    new() {
                        DisplayName = "RSI Periods",
                        ParamName = "rsiPeriods",
                        DataType = "int",
                        DefaultValue = 14,
                        Minimum = 1,
                        Maximum = 250
                    },
                    new() {
                        DisplayName = "Stochastic Periods",
                        ParamName = "stochPeriods",
                        DataType = "int",
                        DefaultValue = 14,
                        Minimum = 1,
                        Maximum = 250
                    },
                    new() {
                        DisplayName = "Signal Periods",
                        ParamName = "signalPeriods",
                        DataType = "int",
                        DefaultValue = 3,
                        Minimum = 1,
                        Maximum = 50
                    },
                    new() {
                        DisplayName = "Smooth Periods",
                        ParamName = "smoothPeriods",
                        DataType = "int",
                        DefaultValue = 1,
                        Minimum = 1,
                        Maximum = 50
                    }
                ],
                Results = [
                    new() {
                        DisplayName = "Oscillator",
                        TooltipTemplate = "StochRSI Oscillator",
                        DataName = "stochRsi",
                        DataType = "number",
                        LineType = "solid",
                        DefaultColor = ChartColors.StandardBlue
                    },
                    new() {
                        DisplayName = "Signal line",
                        TooltipTemplate = "Signal line",
                        DataName = "signal",
                        DataType = "number",
                        LineType = "solid",
                        DefaultColor = ChartColors.StandardRed
                    }
                ]
            },

            // SuperTrend
            new IndicatorListing {
                Name = "SuperTrend",
                Uiid = "SUPERTREND",
                LegendTemplate = "SUPERTREND([P1],[P2])",
                Endpoint = $"{baseUrl}/SUPERTREND/",
                Category = "price-trend",
                ChartType = "overlay",
                Order = Order.Front,
                Parameters =
                [
                    new() {
                        DisplayName = "Lookback Periods",
                        ParamName = "lookbackPeriods",
                        DataType = "int",
                        DefaultValue = 10,
                        Minimum = 1,
                        Maximum = 50
                    },
                    new() {
                        DisplayName = "Multiplier",
                        ParamName = "multiplier",
                        DataType = "number",
                        DefaultValue = 3,
                        Minimum = 0.1,
                        Maximum = 10
                    }
                ],
                Results = [
                    new() {
                        DisplayName = "Upper Band",
                        TooltipTemplate = "SUPERTREND([P1],[P2]) Upper Band",
                        DataName = "upperBand",
                        DataType = "number",
                        LineType = "solid",
                        DefaultColor = ChartColors.StandardRed
                    },
                    new() {
                        DisplayName = "Lower Band",
                        TooltipTemplate = "SUPERTREND([P1],[P2]) Lower Band",
                        DataName = "lowerBand",
                        DataType = "number",
                        LineType = "solid",
                        DefaultColor = ChartColors.StandardGreen
                    },
                    new() {
                        DisplayName = "Transition line",
                        TooltipTemplate = "SUPERTREND([P1],[P2]) Transition Line",
                        DataName = "superTrend",
                        DataType = "number",
                        LineType = "dash",
                        LineWidth = 1,
                        DefaultColor = ChartColors.DarkGrayTransparent
                    }
                ]
            },

            // Ulcer Index
            new IndicatorListing {
                Name = "Ulcer Index (UI)",
                Uiid = "ULCER",
                LegendTemplate = "ULCER([P1])",
                Endpoint = $"{baseUrl}/ULCER/",
                Category = "price-characteristic",
                ChartType = "oscillator",
                Parameters =
                [
                    new() {
                        DisplayName = "Lookback Periods",
                        ParamName = "lookbackPeriods",
                        DataType = "int",
                        DefaultValue = 14,
                        Minimum = 1,
                        Maximum = 250
                    }
                ],
                Results = [
                    new() {
                        DisplayName = "Ulcer Index",
                        TooltipTemplate = "UI([P1])",
                        DataName = "ui",
                        DataType = "number",
                        LineType = "solid",
                        DefaultColor = ChartColors.StandardBlue
                    }
                ]
            },

            // Vortex Indicator
            new IndicatorListing {
                Name = "Vortex Indicator",
                Uiid = "VORTEX",
                LegendTemplate = "VORTEX([P1])",
                Endpoint = $"{baseUrl}/VORTEX/",
                Category = "price-trend",
                ChartType = "oscillator",
                Parameters =
                [
                    new() {
                        DisplayName = "Lookback Periods",
                        ParamName = "lookbackPeriods",
                        DataType = "int",
                        DefaultValue = 14,
                        Minimum = 2,
                        Maximum = 100
                    }
                ],
                Results = [
                    new() {
                        DisplayName = "VI+",
                        TooltipTemplate = "VI+",
                        DataName = "pvi",
                        DataType = "number",
                        LineType = "solid",
                        DefaultColor = ChartColors.StandardGreen
                    },
                    new() {
                        DisplayName = "VI+",
                        TooltipTemplate = "VI-",
                        DataName = "nvi",
                        DataType = "number",
                        LineType = "solid",
                        DefaultColor = ChartColors.StandardRed
                    }
                ]
            },

            // Williams Alligator
            new IndicatorListing {
                Name = "Williams Alligator",
                Uiid = "ALLIGATOR",
                LegendTemplate = "ALLIGATOR([P1],[P2],[P3],[P4],[P5],[P6])",
                Endpoint = $"{baseUrl}/ALLIGATOR/",
                Category = "price-trend",
                ChartType = "overlay",
                Parameters =
                [
                    new() {
                        DisplayName = "Jaw Periods",
                        ParamName = "jawPeriods",
                        DataType = "int",
                        DefaultValue = 13,
                        Minimum = 1,
                        Maximum = 250
                    },
                    new() {
                        DisplayName = "Jaw Offset",
                        ParamName = "jawOffset",
                        DataType = "int",
                        DefaultValue = 8,
                        Minimum = 1,
                        Maximum = 30
                    },
                    new() {
                        DisplayName = "Teeth Periods",
                        ParamName = "teethPeriods",
                        DataType = "int",
                        DefaultValue = 8,
                        Minimum = 1,
                        Maximum = 250
                    },
                    new() {
                        DisplayName = "Teeth Offset",
                        ParamName = "teethOffset",
                        DataType = "int",
                        DefaultValue = 5,
                        Minimum = 1,
                        Maximum = 30
                    },
                    new() {
                        DisplayName = "Lips Periods",
                        ParamName = "lipsPeriods",
                        DataType = "int",
                        DefaultValue = 5,
                        Minimum = 1,
                        Maximum = 250
                    },
                    new() {
                        DisplayName = "Lips Offset",
                        ParamName = "lipsOffset",
                        DataType = "int",
                        DefaultValue = 3,
                        Minimum = 1,
                        Maximum = 30
                    }
                ],
                Results = [
                    new() {
                        DisplayName = "Jaw",
                        TooltipTemplate = "Jaw([P1]/[P2])",
                        DataName = "jaw",
                        DataType = "number",
                        LineType = "solid",
                        DefaultColor = ChartColors.StandardBlue
                    },
                    new() {
                        DisplayName = "Teeth",
                        TooltipTemplate = "Teeth([P3]/[P4])",
                        DataName = "teeth",
                        DataType = "number",
                        LineType = "solid",
                        DefaultColor = ChartColors.StandardRed
                    },
                    new() {
                        DisplayName = "Lips",
                        TooltipTemplate = "Lips([P4]/[P5])",
                        DataName = "lips",
                        DataType = "number",
                        LineType = "solid",
                        DefaultColor = ChartColors.StandardGreen
                    }
                ]
            },

            // Zig Zag (close)
            new IndicatorListing {
                Name = "Zig Zag (close)",
                Uiid = "ZIGZAG-CL",
                LegendTemplate = "ZIGZAG([P1]% CLOSE)",
                Endpoint = $"{baseUrl}/ZIGZAG-CLOSE/",
                Category = "price-transform",
                ChartType = "overlay",
                Parameters =
                [
                    new() {
                        DisplayName = "Percent Change",
                        ParamName = "percentChange",
                        DataType = "number",
                        DefaultValue = 5,
                        Minimum = 1,
                        Maximum = 200
                    }
                ],
                Results = [
                    new() {
                        DisplayName = "Zig Zag",
                        TooltipTemplate = "ZIGZAG([P1]% CLOSE)",
                        DataName = "zigZag",
                        DataType = "number",
                        LineType = "solid",
                        DefaultColor = ChartColors.StandardBlue
                    },
                    new() {
                        DisplayName = "Zig Zag Retrace High",
                        TooltipTemplate = "ZIGZAG([P1]% CLOSE) Retrace High",
                        DataName = "retraceHigh",
                        DataType = "number",
                        LineType = "dash",
                        DefaultColor = ChartColors.ThresholdGrayTransparent
                    },
                    new() {
                        DisplayName = "Zig Zag Retrace Low",
                        TooltipTemplate = "ZIGZAG([P1]% CLOSE) Retrace Low",
                        DataName = "retraceLow",
                        DataType = "number",
                        LineType = "dash",
                        DefaultColor = ChartColors.ThresholdGrayTransparent
                    }
                ]
            },

            // Zig Zag (high/low)
            new IndicatorListing {
                Name = "Zig Zag (high/low)",
                Uiid = "ZIGZAG-HL",
                LegendTemplate = "ZIGZAG([P1]% HIGH/LOW)",
                Endpoint = $"{baseUrl}/ZIGZAG-HIGHLOW/",
                Category = "price-transform",
                ChartType = "overlay",
                Parameters =
                [
                    new() {
                        DisplayName = "Percent Change",
                        ParamName = "percentChange",
                        DataType = "number",
                        DefaultValue = 5,
                        Minimum = 1,
                        Maximum = 200
                    }
                ],
                Results = [
                    new() {
                        DisplayName = "Zig Zag",
                        TooltipTemplate = "ZIGZAG([P1]% HIGH/LOW)",
                        DataName = "zigZag",
                        DataType = "number",
                        LineType = "solid",
                        DefaultColor = ChartColors.StandardBlue
                    },
                    new() {
                        DisplayName = "Zig Zag Retrace High",
                        TooltipTemplate = "ZIGZAG([P1]% HIGH/LOW) Retrace High",
                        DataName = "retraceHigh",
                        DataType = "number",
                        LineType = "dash",
                        DefaultColor = ChartColors.ThresholdGrayTransparent
                    },
                    new() {
                        DisplayName = "Zig Zag Retrace Low",
                        TooltipTemplate = "ZIGZAG([P1]% HIGH/LOW) Retrace Low",
                        DataName = "retraceLow",
                        DataType = "number",
                        LineType = "dash",
                        DefaultColor = ChartColors.ThresholdGrayTransparent
                    }
                ]
            }
        ];

        return [.. listing.OrderBy(x => x.Name)];
    }
}
