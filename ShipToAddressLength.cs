using System;
using P21.Extensions.BusinessRule;

namespace ShipToAddressLength
{
    public class ShipToAddressLength : Rule
    {
        public override RuleResult Execute()
        {
            RuleResult ruleResult = new RuleResult();

            try
            {
                DataField fieldAddress = Data.Fields.GetFieldByAlias("address_field");

                foreach (DataField field in Data.Fields)
                {
                    if (field.FieldAlias == "address_field")
                    {
                        string addressField = field.FieldValue;
                        string fieldTitle = (fieldAddress.FieldName == "phys_address1") ? "Address 1" : "Address 2";

                        if (addressField.Length > 35)
                        {
                            ruleResult.Message = "The address field, " + fieldTitle + ", is longer than 30 characters. Please use a shorter address and then try again.";
                            ruleResult.Message += "\r\nLength: " + addressField.Length;
                            ruleResult.Success = false;
                            return ruleResult;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ruleResult.Message = "Error in address length!";
                ruleResult.Message += "\r\n Type: " + ex.InnerException;
                ruleResult.Message += "\r\n Source: " + ex.Source;
                ruleResult.Message += "\r\n Details: " + ex.Message;
                ruleResult.Message += "\r\n Stack Trace: " + ex.StackTrace;
                ruleResult.Message += "\r\n Data: " + ex.Data;
                ruleResult.Success = false;
                return ruleResult;
            }
            ruleResult.Success = true;
            return ruleResult;
        }

        public override string GetDescription()
        {
            return "Checks the length of the address fields.";
        }

        public override string GetName()
        {
            return "ShipToAddressLength";
        }
    }
}