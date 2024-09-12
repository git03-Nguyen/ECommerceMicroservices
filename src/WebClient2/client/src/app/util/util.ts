export function getCookie(key: string) {
  const b = document.cookie.match("(^|;)\\s*" + key + "\\s*=\\s*([^;]+)");
  return b ? b.pop() : "";
}

export function currencyFormat(amount: number) {
  return "$" + (amount / 100).toFixed(2);
}

export function vndCurrencyFormat(amount: number) {
  return amount.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",") + " đ";
}
