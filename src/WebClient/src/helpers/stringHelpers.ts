const formatNumberWithCommas = (num: number): string => {
  return num.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
}

const formatNumberWithSpaces = (num: number): string => {
  return num.toString().replace(/\B(?=(\d{3})+(?!\d))/g, " ");
}

const getFirstCharOfName = (fullName: string): string => {
  const firstLetters = fullName.split(" ").map((name) => name[0]);
  const res = firstLetters[firstLetters.length - 1]?.toUpperCase();
  if (!res) {
    return "A";
  }
  return res;
}

export { formatNumberWithCommas, formatNumberWithSpaces, getFirstCharOfName };