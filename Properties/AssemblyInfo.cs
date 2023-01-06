﻿using MelonLoader;
using BestCombinationSuggest;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using static BestCombinationSuggest.ModConstants;

// 有关程序集的一般信息由以下
// 控制。更改这些特性值可修改
// 与程序集关联的信息。
[assembly: AssemblyTitle(NAME)]
[assembly: AssemblyDescription(DESCRIPTION)]
[assembly: AssemblyProduct(NAME)]
[assembly: AssemblyCopyright(COPYRIGHT)]
[assembly: AssemblyVersion(VERSION)]
[assembly: AssemblyFileVersion(VERSION)]

// 将 ComVisible 设置为 false 会使此程序集中的类型
//对 COM 组件不可见。如果需要从 COM 访问此程序集中的类型
//请将此类型的 ComVisible 特性设置为 true。
[assembly: ComVisible(false)]

// 如果此项目向 COM 公开，则下列 GUID 用于类型库的 ID
[assembly: Guid("f5da3fba-a958-46b0-9326-ac78817ce849")]

[assembly: MelonInfo(typeof(BestCombinationMelon), NAME, VERSION, AUTHOR)]
[assembly: MelonGame("PeroPeroGames", "MuseDash")]