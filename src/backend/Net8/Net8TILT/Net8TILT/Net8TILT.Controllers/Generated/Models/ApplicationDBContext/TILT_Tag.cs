﻿//4.this was autogenerated by a tool. Do not modify! Use partial
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

//modified 2023.12.15
namespace Generated;
[ApiController]
[Route("api/[controller]")]    
public partial class REST_ApplicationDBContext_TILT_TagController : Controller
{
    private ApplicationDBContext _context;
    public REST_ApplicationDBContext_TILT_TagController(ApplicationDBContext context)
	{
        _context=context;
	}
    [HttpGet]
    public async Task<TILT_Tag_Table[]> Get(){
        var data= await _context.TILT_Tag.ToArrayAsync();
        var ret = data.Select(it => (TILT_Tag_Table)it!).ToArray();
        return ret;

        
    }
    }    
