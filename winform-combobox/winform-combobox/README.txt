目前有一个bug，就是使用键盘的delete键删除combobox中的最后一项时，异常。





此用例演示了如何动态调整combobox下拉列表的高度，项的高度。人工绘制combobox中的项。
1.DrawMode设置为OwnerDrawVariable
2.响应DrawItem事件，每次windows绘制combobox中的项时，会调用此方法。传递的参数是此项在下拉列表中的区域坐标信息和项在combobox集合中的索引
3.响应MeasureItem时间，每次windows绘制combobox中的项时，会调用此方法，计算需要绘制项时时使用的高度和宽度
4.根据项目个数，改变下拉列表的高度








