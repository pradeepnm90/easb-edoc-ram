using System.ComponentModel;

namespace Markel.Pricing.Service.Infrastructure.Constants
{
    public enum ControlTypesEnum
    {
        [Description("checkbox input")]
        CHECKBOX_INPUT = 20,
        [Description("direct date input")]
        DATE_INPUT = 30,
        [Description("date input with calendar")]
        DATE_INPUT_WITH_PICKER = 31,
        [Description("multi select dropdown")]
        DROP_DOWN_MULTI_SELECT = 52,
        [Description("single select dropdown")]
        DROP_DOWN_SINGLE_SELECT = 50,
        [Description("single select dropdown with input on the right")]
        DROP_DOWN_SINGLE_SELECT_WITH_INPUT = 51,
        [Description("hidden field")]
        HIDDEN_FIELD = 40,
        [Description("Info icon")]
        INFO_ICON = 70,
        [Description("label")]
        LABEL_NORMAL = 1,
        [Description("formated number input")]
        NUMBER_INPUT = 4,
        [Description("formated currency with symbol if available")]
        NUMBER_INPUT_CURRENCY = 6,
        [Description("read only field that is sum of grouped fields")]
        NUMBER_INPUT_GROUP_TOTAL = 10,
        [Description("formated latitude input (S/W)")]
        NUMBER_INPUT_LATITUDE = 8,
        [Description("formated longitude input (N/S)")]
        NUMBER_INPUT_LONGITUDE = 9,
        [Description("number input without whole number formatting")]
        NUMBER_INPUT_NO_FORMAT = 5,
        [Description("percent input with percent sign")]
        NUMBER_INPUT_PERCENT = 7,
        [Description("radion input")]
        RADIO_INPUT = 25,
        [Description("text area input")]
        TEXT_AREA_INPUT = 60,
        [Description("text input")]
        TEXT_INPUT = 2,
        [Description("text input box with action button on the right")]
        TEXT_INPUT_WITH_BUTTON = 3,
        [Description("visual group")]
        VG_HEADER = 80,
        [Description("group header")]
        GROUP_HEADER = 81
    }
}
