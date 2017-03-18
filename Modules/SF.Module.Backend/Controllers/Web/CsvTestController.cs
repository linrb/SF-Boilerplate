﻿using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace SF.Module.Backend.Controllers
{
    [Route("api/[controller]")]
    public class CsvTestController : Controller
    {
        // GET api/csvtest
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(DummyData());
        }

        [HttpGet]
        [Route("data.csv")]
        [Produces("text/csv")]
        public IActionResult GetDataAsCsv()
        {
            return Ok(DummyData());
        }

        private static IEnumerable<LocalizationRecord> DummyData()
        {
            var model = new List<LocalizationRecord>
            {
                new LocalizationRecord
                {
                    Id = 1,
                    Key = "test",
                    Text = "test text",
                    LocalizationCulture = "en-US",
                    ResourceKey = "test"

                },
                new LocalizationRecord
                {
                    Id = 2,
                    Key = "test",
                    Text = "test2 text de-CH",
                    LocalizationCulture = "de-CH",
                    ResourceKey = "test"

                }
            };

            return model;
        }

        // POST api/csvtest/import
        [HttpPost]
        [Route("import")]
        public IActionResult Import([FromBody]List<LocalizationRecord> value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                List<LocalizationRecord> data = value;
                return Ok();
            }
        }

    }
    public class LocalizationRecord
    {
        public long Id { get; set; }

        public string Key { get; set; }

        public string Text { get; set; }

        public string LocalizationCulture { get; set; }
        public string ResourceKey { get; set; }
    }
}
