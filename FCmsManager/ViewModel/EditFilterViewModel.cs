﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using FCms.Content;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FCmsManager.ViewModel
{
    public class EditFilterViewModel
    {
        String[] TypeNames = new[] { "Text", "Boolean", "RegEx", "DateRange", "ValueList" };

        public Guid? Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public String Type { get; set; }

        public List<string> Values { get; set; }

        public EditFilterViewModel()
        {
            Values = new List<string>();
        }

        public IEnumerable<SelectListItem> TypeList {
            get {
                foreach (string typename in TypeNames)
                {
                    yield return new SelectListItem { Text = typename, Value = typename };
                }
            }
        }

        public IFilter MapToModel(IFilter model)
        {
            model.Name = this.Name;
            model.Id = this.Id ?? Guid.NewGuid();
            if (model is ValueListFilter) { 
                (model as ValueListFilter).Values.AddRange(this.Values);
            }
            return model;
        }

        public void MapToFrom(IFilter model)
        {
            this.Name = model.Name;
            this.Id = model.Id;
            this.Type = model.Type;
            if (model is ValueListFilter)
            {
                this.Values.AddRange((model as ValueListFilter).Values);
            }
        }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            yield break;
        }
    }
}
