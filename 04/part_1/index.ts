/**
 * Return coordinates of X
 *
 * @param matrix matrix of characters
 * @param width width of matrix
 * @param height height of matrix
 * @returns
 */
function findXCoords(matrix: string[][], width: number, height: number) {
	let coords: number[][] = [];
	for (let y = 0; y < height; y++) {
		for (let x = 0; x < width; x++) {
			if (matrix[y][x] == "X") {
				coords.push([x, y]);
			}
		}
	}

	return coords;
}

function isValidCoordinate(
	x: number,
	y: number,
	width: number,
	height: number
) {
	return !(x >= width || x < 0 || y >= height || y < 0);
}

async function main() {
	const fileContent = Bun.file("../input.txt");

	const fileContentText = await fileContent.text();

	const matrix: string[][] = fileContentText
		.split("\n")
		.map((x) => x.split(""))
		.slice(0, -1);

	const matrixWidth = matrix[0].length;
	const matrixHeight = matrix.length;

	const XCoordinates = findXCoords(matrix, matrixWidth, matrixHeight);

	const KEYWORD = "XMAS";

	const directions = [
		[0, 1],
		[0, -1],
		[1, 1],
		[1, 0],
		[1, -1],
		[-1, 1],
		[-1, 0],
		[-1, -1],
	];

	let totalCount = 0;

	for (let coordinates of XCoordinates) {
		for (let direction of directions) {
			let valid = true;
			for (let i = 1; i < 4; i++) {
				let x: number = direction[0] * i + coordinates[0];
				let y: number = direction[1] * i + coordinates[1];

				if (!isValidCoordinate(x, y, matrixWidth, matrixHeight)) {
					valid = false;
					break;
				}

				if (matrix[y][x] != KEYWORD[i]) {
					valid = false;
					break;
				}
			}

			if (valid) totalCount++;
		}
	}

	console.log(totalCount);
}

main();
