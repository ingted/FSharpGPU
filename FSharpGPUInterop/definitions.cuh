/*This file is part of FSharpGPU.

	FSharpGPU is free software : you can redistribute it and / or modify
	it under the terms of the GNU General Public License as published by
	the Free Software Foundation, either version 3 of the License, or
	(at your option) any later version.

	FSharpGPU is distributed in the hope that it will be useful,
	but WITHOUT ANY WARRANTY; without even the implied warranty of
	MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.See the
	GNU General Public License for more details.

	You should have received a copy of the GNU General Public License
	along with FSharpGPU.If not, see <http://www.gnu.org/licenses/>.
*/

/* This software contains source code provided by NVIDIA Corporation. */

/*Copyright � 2015 Philip Curzon */

#pragma once

const int MAX_BLOCKS = 32768;
const int MAX_THREADS = 256;

struct ThreadBlocks{
	int threadCount;
	int blockCount;
	int thrBlockCount;
	int loopCount;
	int N;
};