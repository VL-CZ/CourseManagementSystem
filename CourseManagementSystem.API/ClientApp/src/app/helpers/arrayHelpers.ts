export class ArrayHelpers {

  /**
   * resize array to the given size (modifies the array!)
   * - if the given size is smaller or equal to the current size, resize array to the first n items
   * - otherwise extend it to the given size - add {@instanceToAdd} items at the end
   * @param array
   * @param newSize
   * @param instanceToAdd
   */
  public static resize<T>(array: T[], newSize: number, instanceToAdd: T): void {
    const oldSize = array.length;

    if (oldSize > newSize) {
      for (let size = oldSize; size > newSize; size--) {
        array.pop();
      }
    } else {
      for (let size = oldSize; size < newSize; size++) {
        array.push(instanceToAdd);
      }
    }
  }
}

