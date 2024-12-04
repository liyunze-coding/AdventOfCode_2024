/**
 * Return coordinates of X
 *
 * @param matrix matrix of characters
 * @param width width of matrix
 * @param height height of matrix
 * @returns
 */
function findCharCoords(
	matrix: string[][],
	width: number,
	height: number,
	character: string
) {
	let coords: number[][] = [];
	for (let y = 0; y < height; y++) {
		for (let x = 0; x < width; x++) {
			if (matrix[y][x] == character) {
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

	const ACoordinates = findCharCoords(matrix, matrixWidth, matrixHeight, "A");

	const chars = ["M", "S"];

	let totalCount = 0;

	for (let coord of ACoordinates) {
		let x = coord[0];
		let y = coord[1];

		if (
			isValidCoordinate(x - 1, y - 1, matrixWidth, matrixHeight) &&
			isValidCoordinate(x + 1, y + 1, matrixWidth, matrixHeight) &&
			isValidCoordinate(x + 1, y - 1, matrixWidth, matrixHeight) &&
			isValidCoordinate(x - 1, y + 1, matrixWidth, matrixHeight)
		) {
			let topLeft = matrix[y - 1][x - 1];
			let bottomRight = matrix[y + 1][x + 1];
			let topRight = matrix[y - 1][x + 1];
			let bottomLeft = matrix[y + 1][x - 1];

			if (
				// Check if "\" is MAS
				chars.includes(topLeft) &&
				chars.includes(bottomRight) &&
				topLeft != bottomRight &&
				// Check if "/" is MAS
				chars.includes(topRight) &&
				chars.includes(bottomLeft) &&
				topRight != bottomLeft
			) {
				totalCount++;
			}
		}
	}
	console.log(totalCount);
}

main();
